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
        public ActionResult StartGame(string username)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(username))
                {
                    Session["username"] = username;
                    Session["IsFiftyFiftyUsed"] = false;
                    Session["IsAudienceAskUsed"] = false;
                    Session["IsPhoneCallUsed"] = false;
                    return NextQuestion(0);
                    //return RedirectToAction("NextQuestion", new {questionNumber = 0});
                }
                ModelState.AddModelError("usernameError", "Please, input username");
                return View("Index");
            }

            return View("Index");
        }
        


        [HttpPost]
        public ActionResult NextQuestion(int questionNumber)
        {
            questionNumber++;
            if (questionNumber == 5)
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
            model.QuestionId = currentQuestion.QuestionId;
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

        public ActionResult FiftyFifty(int questionId)
        {
            var question = _repository.GetQuestionById(questionId);

            QuestionModel model = new QuestionModel
            {
                QuestionId = questionId,
                QuestionText = question.QuestionText,
                QuestionNumber = question.Difficulty,
                AnswerId = question.Answer - 1,
               
                Options = new List<string>
                { question.Option1, question.Option2, question.Option3, question.Option4}
            };
            model.AnswerText = model.Options[question.Answer - 1];
            var randomList = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (i != model.AnswerId)
                {
                    randomList.Add(i);
                }
            }
            Random random = new Random();
            randomList.RemoveAt(random.Next(0,randomList.Count));

            model.Options[randomList[0]] = "";
            model.Options[randomList[1]] = "";
            Session["IsFiftyFiftyUsed"] = true;
            return View("StartGame", model);

        }
    }
}