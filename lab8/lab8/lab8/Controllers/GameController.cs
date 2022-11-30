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

        [Route("Game/Game")]
        public IActionResult Game()
        {
            return Redirect(Url.Link("game_url", new { }));
        }

        [Route("/Set", Name = "game_url")]
        public IActionResult Set()
        {
            return View("Game");
        }

        [Route("/Set,{value}")]
        public IActionResult Set(int value)
        {
            ViewBag.n = value;
            amountN = value;
            return View("Game");
        }

        [Route("/Draw")]
        public IActionResult Draw()
        {
            Random rnd = new Random();

            if(amountN != null)
                randValue = rnd.Next(0, (int)amountN - 1);

            ViewBag.compNumb = randValue;

            return View("Game");
        }

        [Route("/Guess,{value}")]
        public IActionResult Guess(int value)
        {
            if (value == randValue) ViewBag.result = 1;
            else if (value < randValue) ViewBag.result = 0;
            else ViewBag.result = 2;

            return View("Game");
        }
    }
}

