using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using lab12.Data;
using lab12.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace lab12.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly lab12.Data.MyDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CreateModel(lab12.Data.MyDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Article.Picture != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                string newUniqueName = Guid.NewGuid().ToString() + "_" + Article.Picture.FileName;

                using (FileStream newFile = new FileStream(Path.Combine(uploadFolder, newUniqueName), FileMode.Create))
                {
                    Article.Picture.CopyTo(newFile);
                }

                Article.FilePath = newUniqueName;
            }

            _context.Article.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
