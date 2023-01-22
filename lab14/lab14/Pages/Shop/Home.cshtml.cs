using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab14.Data;
using lab14.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab14.Pages.Shop
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public Article Article { get; set; }

        public IRepository Repository { get; set; }
        private readonly lab14.Data.MyDbContext _context;
        public HomeModel(lab14.Data.MyDbContext context, IRepository repo)
        {
            _context = context;
            Repository = repo;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return Page();
        }

       public async Task<IActionResult> OnPostAddArticleAsync()
        {
            await Repository.AddArticleAsync(Article);
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }
    }
}
