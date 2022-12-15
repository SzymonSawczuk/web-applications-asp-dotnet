using lab10.Data;
using lab10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab10.Controllers
{
    public class ShopController : Controller
    {
        private readonly MyDbContext _context;

        public ShopController(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.activeElem = -1;
            ViewBag.articles = await _context.Article.ToListAsync();

            return View(await _context.Category.ToListAsync());
        }

        [HttpGet("/Shop/Index/{categoryId}"), ActionName("Index")]
        public async Task<IActionResult> Index(int categoryId)
        {
            ViewBag.activeElem = categoryId;
            ViewBag.articles = await _context.Article.Where(article => article.CategoryId == categoryId).ToListAsync();
            return View(await _context.Category.ToListAsync());
        }
    }
}
