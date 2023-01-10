using lab13.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab13.Pages.Shop
{
    public class DetailsModel : PageModel
    {
        private readonly lab13.Data.MyDbContext _context;

        public DetailsModel(lab13.Data.MyDbContext context)
        {
            _context = context;
        }

        public Article Article { get; set; }
        public int? backId;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            backId = HttpContext.Session.GetInt32("activeElem");
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.Article
                .Include(a => a.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Article == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
