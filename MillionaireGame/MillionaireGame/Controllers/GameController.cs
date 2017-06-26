using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MillionaireGame.DAL.Entities;
using MillionaireGame.DAL.Repository;
using MillionaireGame.Models;
using MillionaireGame.BLL;
using MillionaireGame.Infrastructure.Filters;

namespace MillionaireGame.Controllers
{
    [ExceptionLogger]
    public class GameController : Controller
    {
        private IQuestionRepository _questionRepository;
        private IGameService _gameService;
        private Random _random = new Random();

        public GameController(IQuestionRepository questionRepository, IGameService gameService)
        {
            _questionRepository = questionRepository;
            _gameService = gameService;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult StartGame(string username)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(username))
                {
                    Session["QuestionNumber"] = 1;
                    Session["username"] = username;
                    return View("StartGame", FormMoneyDictionary());
                }
                ModelState.AddModelError("usernameError", "Please, input username");
                return View("Index");
            }

            return View("Index");
        }

        public Dictionary<string, string> FormMoneyDictionary()
        {
            var dictionary = new Dictionary<string, string>
            {
                {"15", "1 000 000"},
                {"14", "500 000"},
                {"13", "250 000"},
                {"12", "125 000"},
                {"11", "64 000"},
                {"10", "32 000"},
                {"9", "16 000"},
                {"8", "8 000"},
                {"7", "4 000"},
                {"6", "2 000"},
                {"5", "1 000"},
                {"4", "500"},
                {"3", "300"},
                {"2", "200"},
                {"1", "100"}
            };
            return dictionary;
        }

        public ActionResult NextQuestion()
        {
            int questionNumber = (int)Session["QuestionNumber"];
            var allQuestionsFromLevel = _questionRepository.GetQuestions().Where(x => x.Difficulty.Level == questionNumber).ToList();
            var currentQuestion = allQuestionsFromLevel[_random.Next(0, allQuestionsFromLevel.Count)];
            QuestionModel model = GetQuestionModel(currentQuestion);
            Session["QuestionNumber"] = questionNumber + 1;

            return PartialView("QuestionView", model);
        }


        public int CheckAnswer(int questionId, int selectedOption)
        {
            var question = _questionRepository.GetQuestionById(questionId);
            _questionRepository.InsertRecord(
                new Record
                {
                    Question = question,
                    Answer = selectedOption,
                    Username = Session["username"].ToString()
                });
            _questionRepository.Save();
            return question.Answer;
        }

        public QuestionModel GetQuestionModel(Question question)
        {
            QuestionModel model = new QuestionModel
            {
                QuestionText = question.QuestionText,
                Options = new List<string>(),
                QuestionId = question.QuestionId
            };

            model.Options.Add(question.Option1);
            model.Options.Add(question.Option2);
            model.Options.Add(question.Option3);
            model.Options.Add(question.Option4);

            model.QuestionNumber = question.Difficulty.Level;

            return model;
        }

        public ActionResult Victory()
        {
            return View("Victory");
        }

        public ActionResult Failure()
        {
            return View("Failure");
        }

        public ActionResult FiftyFifty(int questionId)
        {
            var question = _questionRepository.GetQuestionById(questionId);

            QuestionModel model = GetQuestionModel(question);

            var allAnswersForQuestion = _questionRepository.GetRecords().ToList().Where(x => x.Question == question && x.Answer != question.Answer).ToList();
            if (allAnswersForQuestion.Any())
            {
                var groupedRecords = from record in allAnswersForQuestion
                                     group record.Question by record.Answer
                    into g
                                     select new { Answer = g.Key, AmountOfRecords = g.Count() };
                var mostPopularAnswer = groupedRecords.OrderByDescending(x => x.AmountOfRecords).First().Answer;

                for (int i = 0; i < 4; i++)
                {
                    if (i != question.Answer - 1 && i != mostPopularAnswer - 1)
                    {
                        model.Options[i] = "";
                    }
                }

            }
            else
            {
                var randomList = new List<int>();
                for (int i = 0; i < 4; i++)
                {
                    if (i != question.Answer - 1)
                    {
                        randomList.Add(i);
                    }
                }
                Random random = new Random();
                randomList.RemoveAt(random.Next(0, randomList.Count));

                model.Options[randomList[0]] = "";
                model.Options[randomList[1]] = "";
            }

            return PartialView("QuestionView", model);
        }

        public ActionResult AskAudience(int questionId)
        {
            var question = _questionRepository.GetQuestionById(questionId);
            var allAnswersForQuestion = _questionRepository.GetRecords().ToList().Where(x => x.Question == question).ToList();

            var dictinary = new Dictionary<string, int>();
            var options = new List<string>
            {
                question.Option1, question.Option2, question.Option3, question.Option4
            };

            int numberOfASymbol = 65;

            if (allAnswersForQuestion.Any())
            {
                var groupedRecords = (from record in allAnswersForQuestion
                                      group record.Question by record.Answer
                    into g
                                      select new { Answer = g.Key, AmountOfRecords = g.Count() }).ToList();

                for (int i = 1; i <= 4; i++)
                {
                    if (!groupedRecords.Exists(x => x.Answer == i))
                    {
                        groupedRecords.Add(new { Answer = i, AmountOfRecords = 0 });
                    }
                }

                var totalAmountOfRecords = allAnswersForQuestion.Count;

                foreach (var element in groupedRecords.OrderBy(x => x.Answer))
                {
                    double percentage = (double)element.AmountOfRecords / totalAmountOfRecords * 100.0;
                    dictinary.Add($"{(char)numberOfASymbol}: {options[element.Answer - 1]}", (int)percentage);
                }
            }
            else
            {
                var percentageForAnswer = _random.Next(40, 101);
                var restPercentage = 100 - percentageForAnswer;

                for (int i = 0; i < 4; i++)
                {
                    var text = $"{(char)numberOfASymbol}: {options[i]}";
                    if (i == question.Answer - 1)
                    {
                        dictinary.Add(text, percentageForAnswer);
                    }
                    else if (i == 3)
                    {
                        dictinary.Add(text, restPercentage);
                    }
                    else
                    {
                        var currentPercentage = _random.Next(0, restPercentage);
                        dictinary.Add(text, currentPercentage);
                        restPercentage -= currentPercentage;
                    }
                    numberOfASymbol++;
                }
            }

            return PartialView("AskAudiencePartial", dictinary);
        }

        public ActionResult ShowEmailView(int questionId)
        {
            MessageModel model = new MessageModel
            {
                QuestionId = questionId
            };

            return View("SendEmailPartial", model);
        }

        public async Task<ActionResult> SendEmailToFriend(MessageModel model)
        {
            var question = _questionRepository.GetQuestionById(model.QuestionId);
            var messageText = FormTextForMessage(question);

            var message = new MailMessage();
            message.To.Add(new MailAddress(model.Email));
            message.Subject = "Millionaire game";
            message.Body = messageText;
            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
                return new HttpStatusCodeResult(200);
            }
        }

        public string FormTextForMessage(Question question)
        {
            StringBuilder messageText = new StringBuilder();
            messageText.Append("Hello!" + Environment.NewLine);
            messageText.Append($"Your friend {Session["username"]} needs help in Game 'Who wants to be a Millionaire'" + Environment.NewLine);
            messageText.Append("Question:" + Environment.NewLine);
            messageText.Append(question.QuestionText + Environment.NewLine);
            messageText.Append($"A: {question.Option1}" + Environment.NewLine);
            messageText.Append($"B: {question.Option2}" + Environment.NewLine);
            messageText.Append($"C: {question.Option3}" + Environment.NewLine);
            messageText.Append($"D: {question.Option4}" + Environment.NewLine);
            return messageText.ToString();
        }


    }
}