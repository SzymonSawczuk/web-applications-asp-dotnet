using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace lab8.Controllers
{
    public class GameController : Controller
    {
        static int? amountN = null;
        static int? randValue = null;
        static int tryNumber = 1;
        static bool isGuessed = false;

        [Route("Game/Game")]
        public IActionResult Game()
        {
            return Redirect(Url.Link("game_url", new { }));
        }

        [Route("/Set", Name = "game_url")]
        public IActionResult Set()
        {
            ViewBag.ShowClass = "start";
            isGuessed = false;
            tryNumber = 1;
            return View("Game");
        }

        public IActionResult Set2(int value)
        {
            return Redirect(Url.Link("game_url_value", new { value = value }));
        }

        [Route("/Set,{value}", Name = "game_url_value")]
        public IActionResult Set(int value)
        {
            ViewBag.n = value;
            amountN = value;
            randValue = null;
            ViewBag.ShowClass = "set_done";
            isGuessed = false;
            tryNumber = 1;
            return View("Game");
        }

        [Route("/Draw")]
        public IActionResult Draw()
        {
            Random rnd = new Random();

            if(amountN != null)
                randValue = rnd.Next(0, (int)amountN - 1);

            ViewBag.compNumb = randValue;

            ViewBag.ShowClass = "drawed";

            isGuessed = false;
            tryNumber = 1;

            return View("Game");
        }

        public IActionResult Guess2(int value)
        {
            return Redirect(Url.Link("guess_url", new { value = value }));
        }

        [Route("/Guess,{value}", Name = "guess_url")]
        public IActionResult Guess(int value)
        {
            if (isGuessed || value == randValue)
            {
                ViewBag.ShowClass = "drawed guessed";
            }
            else if (!isGuessed && value < randValue)
            {
                ViewBag.ShowClass = "drawed larger";
                if(!isGuessed) tryNumber++;
            }
            else if(!isGuessed && value > randValue)
            {
                ViewBag.ShowClass = "drawed smaller";
                if(!isGuessed) tryNumber++;
            }

            ViewBag.tryNum = tryNumber;

            return View("Game");
        }
    }
}

