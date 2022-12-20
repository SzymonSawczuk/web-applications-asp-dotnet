using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace lab8.Controllers
{
    public class GameController : Controller
    {
        [Route("Game/Game")]
        public IActionResult Game()
        {
            return Redirect(Url.Link("game_url", new { }));
        }

        [Route("/Set", Name = "game_url")]
        public IActionResult Set()
        {
            ViewBag.ShowClass = "start";
            HttpContext.Session.SetString("isGuessed", "false");
            HttpContext.Session.SetInt32("tryNumber", 1);
            return View("Game");
        }

        public IActionResult Set2(int value)
        {
            return Redirect(Url.Link("game_url_value", new { value = value }));
        }

        [Route("/Set,{value}", Name = "game_url_value")]
        public IActionResult Set(int value)
        {
            HttpContext.Session.SetInt32("amountN", value);
            ViewBag.n = (int)HttpContext.Session.GetInt32("amountN");
            HttpContext.Session.SetInt32("randValue", int.MinValue);

            ViewBag.showclass = "set_done";
            HttpContext.Session.SetString("isGuessed", "false");
            HttpContext.Session.SetInt32("tryNumber", 1);
            return View("Game");
        }

        [Route("/Draw")]
        public IActionResult Draw()
        {
            if(HttpContext.Session.GetInt32("amountN") == null ) return Redirect(Url.Link("game_url", new { }));

            Random rnd = new Random();

            if((int)HttpContext.Session.GetInt32("amountN") != -1)
                HttpContext.Session.SetInt32("randValue", rnd.Next(0, (int)HttpContext.Session.GetInt32("amountN") - 1));

            ViewBag.compNumb = (int)HttpContext.Session.GetInt32("randValue");

            ViewBag.ShowClass = "drawed";

            HttpContext.Session.SetString("isGuessed", "false");
            HttpContext.Session.SetInt32("tryNumber", 1);
         

            return View("Game");
        }

        public IActionResult Guess2(int value)
        {
            return Redirect(Url.Link("guess_url", new { value = value }));
        }

        [Route("/Guess,{value}", Name = "guess_url")]
        public IActionResult Guess(int value)
        {
            if(HttpContext.Session.GetInt32("randValue") == null) return Redirect(Url.Link("game_url", new { }));

            int randValue = (int)HttpContext.Session.GetInt32("randValue");
            bool isGuessed = HttpContext.Session.GetString("isGuessed") == "true";
            int tryNumber = (int)HttpContext.Session.GetInt32("tryNumber");
            if (isGuessed || value == randValue)
            {
                ViewBag.ShowClass = "drawed guessed";
                HttpContext.Session.SetString("isGuessed", "true");
            }
            else if (!isGuessed && value < randValue)
            {
                ViewBag.ShowClass = "drawed larger";
                if(!isGuessed) HttpContext.Session.SetInt32("tryNumber", tryNumber + 1);
            }
            else if(!isGuessed && value > randValue)
            {
                ViewBag.ShowClass = "drawed smaller";
                if(!isGuessed) HttpContext.Session.SetInt32("tryNumber", tryNumber + 1);
            }

            ViewBag.tryNum = tryNumber;

            return View("Game");
        }
    }
}

