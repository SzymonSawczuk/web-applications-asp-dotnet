using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab12.Data;
using lab12.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace lab12.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly lab12.Data.MyDbContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public DeleteModel(lab12.Data.MyDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FindAsync(id);
           

            if (Category != null)
            {
                var articles = await _context.Article.Where(a => a.CategoryId == id).ToListAsync();
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");

                foreach (var article in articles)
                {
                    if (article.FilePath != null)
                    {
                        if (System.IO.File.Exists(Path.Combine(uploadFolder, article.FilePath)))
                        {
                            System.IO.File.Delete(Path.Combine(uploadFolder, article.FilePath));
                        }
                    }

                    _context.Article.Remove(article);
                }
                _context.Category.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
