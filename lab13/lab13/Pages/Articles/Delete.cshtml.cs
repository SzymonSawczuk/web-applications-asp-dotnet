using lab13.Data;
using lab13.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace lab13.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly lab13.Data.MyDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DeleteModel(lab13.Data.MyDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.Article.FindAsync(id);

            if (Article != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");

                if (Article.FilePath != null)
                {
                    if (System.IO.File.Exists(Path.Combine(uploadFolder, Article.FilePath)))
                    {
                        System.IO.File.Delete(Path.Combine(uploadFolder, Article.FilePath));
                    }
                }
                _context.Article.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
