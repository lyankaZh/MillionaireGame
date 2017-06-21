using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MillionaireGame.DAL.Entities;
using MillionaireGame.DAL.Repository;
using MillionaireGame.Helpers;
using MillionaireGame.Models;

namespace MillionaireGame.Controllers
{
    public class GameController : Controller
    {
        private IQuestionRepository _repository;
        private IMessageSender _messageSender;
        private Random _random = new Random();

        public GameController(IQuestionRepository repository, IMessageSender sender)
        {
            _repository = repository;
            _messageSender = sender;
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
                    Session["username"] = username;
                    //Session["IsFiftyFiftyUsed"] = false;
                    //Session["IsAudienceAskUsed"] = false;
                    //Session["IsPhoneCallUsed"] = false;
                    Session["QuestionNumber"] = 1;
                    //var model = GetQuestionModel(1);
                    return View("StartGame");
                    //return RedirectToAction("NextQuestion", new {questionNumber = 0});
                }
                ModelState.AddModelError("usernameError", "Please, input username");
                return View("Index");
            }

            return View("Index");
        }


        public ActionResult NextQuestion()
        {
            int questionNumber = (int)Session["QuestionNumber"];
            if (questionNumber == 15)
            {
                return View("EndGameView", 15);
            }
            var allQuestionsFromLevel = _repository.GetQuestions().Where(x => x.Difficulty == questionNumber).ToList();
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
            model.QuestionNumber = question.Difficulty;
            return model;
        }

        public ActionResult EndGame()
        {
            return View("EndGameView", 15);
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

        //public ActionResult CallFriend()
        //{

        //}

        public async Task<ActionResult> SendEmail()
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress("ulyana.zhovtanetska@gmail.com"));
            message.Subject = "Millionaire game";
            message.Body = "Hello";
            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
                return RedirectToAction("EndGame");
            }
        }

        public ActionResult AskAudience()
        {
            throw new NotImplementedException();
        }
    }
}