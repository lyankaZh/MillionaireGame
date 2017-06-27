using System.Collections.Generic;
using System.Web.Mvc;
using MillionaireGame.DAL.Entities;
using MillionaireGame.Models;
using MillionaireGame.BLL;
using MillionaireGame.Infrastructure.Filters;

namespace MillionaireGame.Controllers
{
    [ExceptionLogger]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        
        public GameController(IGameService gameService)
        {
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
                    return View("StartGame", _gameService.GetWins());
                }

                ModelState.AddModelError("usernameError", "Please, input username");
            }

            return View("Index");
        }

        public ActionResult NextQuestion()
        {
            var questionNumber = (int)Session["QuestionNumber"];
            var currentQuestion = _gameService.GetRandomCurrentQuestion(questionNumber);
            QuestionModel model = GetQuestionModel(currentQuestion);
            Session["QuestionNumber"] = questionNumber + 1;
            return PartialView("QuestionView", model);
        }

        public int CheckAnswer(int questionId, int selectedOption)
        {
           _gameService.LogAnswer(questionId, Session["username"].ToString(), selectedOption);
            return _gameService.GetCorrectAnswer(questionId);
        }

        public QuestionModel GetQuestionModel(Question question)
        {
            var model = new QuestionModel
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
            var question = _gameService.GetQuestionById(questionId);
            var model = new QuestionModel
            {
                QuestionText = question.QuestionText,
                QuestionId = questionId,
                Options = _gameService.GetFiftyFiftyOptions(questionId),
                QuestionNumber = question.Difficulty.Level
            };
           
            return PartialView("QuestionView", model);
        }

        public ActionResult AskAudience(int questionId)
        {
            return PartialView("AskAudiencePartial", _gameService.AskAudience(questionId));
        }

        public ActionResult ShowEmailView(int questionId)
        {
            var model = new MessageModel
            {
                QuestionId = questionId
            };

            return View("SendEmailPartial", model);
        }

        public ActionResult SendEmailToFriend(MessageModel model)
        {
            if (ModelState.IsValid)
            {
                _gameService.SendEmail(model.QuestionId, Session["username"].ToString(), model.Email);
                return new HttpStatusCodeResult(200);
            }

            return new HttpStatusCodeResult(400);          
        }
    }
}