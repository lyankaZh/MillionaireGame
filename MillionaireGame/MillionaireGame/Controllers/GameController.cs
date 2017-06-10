using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillionaireGame.Controllers
{
    public class GameController : Controller
    {
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
                    return View("StartGame");
                }
                ModelState.AddModelError("usernameError", "Please, input username");
                return View("Index");
            }

            return View("Index");
        }
    }
}