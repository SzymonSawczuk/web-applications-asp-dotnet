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

        public void createSolveView(float value1 = 0, float value2 = 0, float value3 = 0)
        {
            var (result1, result2) = ToolViewModel.QuadraticFormula(value1, value2, value3);
            (ViewBag.x1, ViewBag.x2) = ($"{result1:0.00}", $"{result2:0.00}");

            if (result1 == null) ViewBag.resultStyle = "noResult";
            else if (result1 == float.PositiveInfinity) ViewBag.resultStyle = "infResult";
            else if (result1 != null && result2 == null) ViewBag.resultStyle = "oneResult";
            else if (result1 != null && result2 != null) ViewBag.resultStyle = "twoResult";

        }

        [Route("Tool/Solve/{a}")]
        public IActionResult Solve(float a)
        {
            createSolveView(a);
            return View();
        }

        [Route("Tool/Solve/{a}/{b}")]
        public IActionResult Solve(float a, float b)
        {
            createSolveView(a, b);
            return View();
        }

        [Route("Tool/Solve/{a}/{b}/{c}", Name = "solve_url")]
        public IActionResult Solve(float a, float b, float c)
        {
            createSolveView(a, b, c);
            return View();
        }
        public IActionResult Solve2(float a, float b, float c)
        {
            return Redirect(Url.Link("solve_url", new { a = a, b =b, c = c}));
        }



        
    }
}

