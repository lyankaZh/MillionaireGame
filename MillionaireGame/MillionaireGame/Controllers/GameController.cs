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
        private List<Question> questions = new List<Question>();
        private int currentQuestion;

        public GameController(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult StartGame(string username)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(username))
                {
                    Session["username"] = username;
                    return RedirectToAction("StartGame");
                }
                ModelState.AddModelError("usernameError", "Please, input username");
                return View("Index");
            }

            return View("Index");
        }

        public ActionResult StartGame()
        {
            for (int i = 1; i <= 15; i++)
            {
                var allQuestionsFromLevel = _repository.GetQuestions().Where(x => x.Difficulty == i).ToList();
                if (!allQuestionsFromLevel.Any())
                {
                    //return another view with error message
                    return View("Index");
                }
                questions.Add(allQuestionsFromLevel[_random.Next(0,allQuestionsFromLevel.Count)]);
            }
            QuestionModel model = new QuestionModel()
            {
                QuestionText = questions[currentQuestion].QuestionText,
                Options = new List<string>()
            };

            model.Options.Add(questions[currentQuestion].Option1);
            model.Options.Add(questions[currentQuestion].Option2);
            model.Options.Add(questions[currentQuestion].Option3);
            model.Options.Add(questions[currentQuestion].Option4);
            model.Answer = model.Options[questions[currentQuestion].Answer - 1];
            return View("StartGame", model);
        }

        public ActionResult NextQuestion()
        {
            return View("StartGame");
        }
           
    }
}