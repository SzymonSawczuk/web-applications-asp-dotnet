using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lab8.Models;


namespace lab8.Controllers
{
    public class ToolController : Controller
    {

        public IActionResult Solve()
        {
            return View();
        }

        [Route("Tool/Solve/{a}")]
        public IActionResult Solve(float a)
        {
            var (result1, result2) = ToolViewModel.QuadraticFormula(a, 0, 0);
            (ViewBag.x1, ViewBag.x2) = (result1, result2);

            return View();
        }

        [Route("Tool/Solve/{a}/{b}")]
        public IActionResult Solve(float a, float b)
        {
            (ViewBag.x1, ViewBag.x2) = ToolViewModel.QuadraticFormula(a, b, 0);
            return View();
        }

        [Route("Tool/Solve/{a}/{b}/{c}")]
        public IActionResult Solve(float a, float b, float c)
        {
            (ViewBag.x1, ViewBag.x2) = ToolViewModel.QuadraticFormula(a, b, c);
            return View();
        }



        
    }
}

