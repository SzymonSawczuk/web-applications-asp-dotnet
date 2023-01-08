using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace lab12.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly lab12.Data.MyDbContext _context;
        public IndexModel(lab12.Data.MyDbContext context)
        {
            _context = context;
        }
        public IList<Category> Category { get; set; }
        public IList<Article> Article { get; set; }

        public int? activeElem = -1;

        public async Task<IActionResult> OnGet(int? id)
        {
            Category = await _context.Category.ToListAsync();
            if (id == null)
            {
                Article = await _context.Article.ToListAsync();
                
                return Page();
            }
            activeElem = id;
            Article = await _context.Article.Where(article => article.CategoryId == id).ToListAsync();
            
            return Page();
        }
    }
}
