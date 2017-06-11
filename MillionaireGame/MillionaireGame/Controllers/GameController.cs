using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MillionaireGame.DAL.Entities;
using MillionaireGame.DAL.Repository;
using MillionaireGame.Models;

namespace MillionaireGame.Controllers
{
    public class GameController : Controller
    {
        private IQuestionRepository _repository;
        private Random _random = new Random();

        public GameController(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult LogUser(string username)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(username))
                {
                    Session["username"] = username;
                    return RedirectToAction("NextQuestion", new {questionNumber = 0});
                }
                ModelState.AddModelError("usernameError", "Please, input username");
                return View("Index");
            }

            return View("Index");
        }

        public ActionResult NextQuestion(int questionNumber)
        {
            questionNumber++;
            if (questionNumber == 3)
            {
                return View("EndGameView", 15);
            }
            var allQuestionsFromLevel = _repository.GetQuestions().Where(x => x.Difficulty == questionNumber).ToList();
            if (!allQuestionsFromLevel.Any())
            {
                //return another view with error message
                return View("Index");
            }
            var currentQuestion = allQuestionsFromLevel[_random.Next(0, allQuestionsFromLevel.Count)];
            QuestionModel model = new QuestionModel()
            {
                QuestionText = currentQuestion.QuestionText,
                Options = new List<string>()
            };

            model.Options.Add(currentQuestion.Option1);
            model.Options.Add(currentQuestion.Option2);
            model.Options.Add(currentQuestion.Option3);
            model.Options.Add(currentQuestion.Option4);
            model.AnswerId = currentQuestion.Answer - 1;
            model.AnswerText = model.Options[currentQuestion.Answer - 1];
            model.QuestionNumber = questionNumber;
            return View("StartGame", model);
        }

        public ActionResult EndGame()
        {
            return View("EndGameView", 15);
        }
    }
}