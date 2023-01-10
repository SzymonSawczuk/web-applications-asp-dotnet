using lab13.Data;
using lab13.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab13.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly lab13.Data.MyDbContext _context;

        public IndexModel(lab13.Data.MyDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get; set; }

        public async Task OnGetAsync()
        {
            Article = await _context.Article
                .Include(a => a.Category).ToListAsync();
        }
    }
}
