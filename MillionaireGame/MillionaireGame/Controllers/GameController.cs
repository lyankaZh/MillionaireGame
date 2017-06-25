using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MillionaireGame.DAL.Entities;
using MillionaireGame.DAL.Repository;
using MillionaireGame.Models;
using MillionaireGame.BLL;

namespace MillionaireGame.Controllers
{
    public class GameController : Controller
    {
        private IQuestionRepository _repository;
        private IGameService _gameService;
        private Random _random = new Random();

        public GameController(IQuestionRepository repository, IGameService gameService)
        {
            _repository = repository;
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
            var allQuestionsFromLevel = _repository.GetQuestions().Where(x => x.Difficulty.Level == questionNumber).ToList();
            var currentQuestion = allQuestionsFromLevel[_random.Next(0, allQuestionsFromLevel.Count)];
            QuestionModel model = GetQuestionModel(currentQuestion);
            Session["QuestionNumber"] = questionNumber + 1;
            return PartialView("QuestionView", model);
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
            model.AnswerId = question.Answer - 1;
            model.AnswerText = model.Options[question.Answer - 1];
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
            var question = _repository.GetQuestionById(questionId);

            QuestionModel model = GetQuestionModel(question);

            var randomList = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (i != model.AnswerId)
                {
                    randomList.Add(i);
                }
            }
            Random random = new Random();
            randomList.RemoveAt(random.Next(0, randomList.Count));

            model.Options[randomList[0]] = "";
            model.Options[randomList[1]] = "";
            Session["IsFiftyFiftyUsed"] = true;
            return PartialView("QuestionView", model);
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
            var question = _repository.GetQuestionById(model.QuestionId);
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

        public ActionResult AskAudience(int questionId)
        {
            var question = _repository.GetQuestionById(questionId);
            var dictinary = new Dictionary<string, int>();
            var options = new List<string>
            {
                question.Option1, question.Option2, question.Option3, question.Option4
            };
            var percentageForAnswer = _random.Next(40, 101);
            var restPercentage = 100 - percentageForAnswer;
            int numberOfASymbol = 65;
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
            return PartialView("AskAudiencePartial", dictinary);
        }
    }
}