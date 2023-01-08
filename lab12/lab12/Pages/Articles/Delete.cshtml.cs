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

namespace lab12.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly lab12.Data.MyDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DeleteModel(lab12.Data.MyDbContext context, IHostingEnvironment hostingEnvironment)
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
