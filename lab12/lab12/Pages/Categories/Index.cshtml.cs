using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab10.Data;
using lab10.Models;

namespace lab12.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly lab10.Data.MyDbContext _context;

        public IndexModel(lab10.Data.MyDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
