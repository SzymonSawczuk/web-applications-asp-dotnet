using lab14.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab14.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly lab14.Data.MyDbContext _context;
        public IndexModel(lab14.Data.MyDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<Category> Category { get; set; }
        [BindProperty]
        public IList<Article> Article { get; set; }

        public int? activeElem = -1;

        public async Task<IActionResult> OnGet(int? id)
        {

            Category = await _context.Category.ToListAsync();
            if (id == null)
            {
                Article = await _context.Article.ToListAsync();
                HttpContext.Session.SetInt32("activeElem", -1);

                return Page();
            }
            activeElem = id;
            Article = await _context.Article.Where(article => article.CategoryId == id).ToListAsync();
            HttpContext.Session.SetInt32("activeElem", (int)id);

            return Page();
        }

        private Dictionary<int, int> _cartCookieDict;

        public IActionResult OnPost()
        {
            var articleId = int.Parse(Request.Form["articleId"]);
            this._cartCookieDict = this.GetCookie("cart");

            if (this._cartCookieDict.ContainsKey(articleId))
            {
                this._cartCookieDict[articleId] += 1;
                SetCookie(this._cartCookieDict);

                return Redirect(HttpContext.Request.Headers["Referer"].ToString());
            }

            this._cartCookieDict.Add(articleId, 1);
            SetCookie(this._cartCookieDict);
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }

        public void SetCookie(Dictionary<int, int> cartCookieDict)
        {

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("cart", JsonConvert.SerializeObject(cartCookieDict), option);
        }

        public Dictionary<int, int> GetCookie(string name)
        {
            string cookie;
            Request.Cookies.TryGetValue(name, out cookie);
            if (cookie == null)
            {
                return new Dictionary<int, int>();
            }
            return JsonConvert.DeserializeObject<Dictionary<int, int>>(Request.Cookies[name]);
        }
    }
}
