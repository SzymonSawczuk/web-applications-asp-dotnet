using lab14.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "NotAdmin")]
    public class CartModel : PageModel
    {
        private readonly lab14.Data.MyDbContext _context;
        private Dictionary<int, int> _cartCookieDict;
        public CartModel(lab14.Data.MyDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<Category> Category { get; set; }
        [BindProperty]
        public IList<CartViewModel> Cart { get; set; }

        public int? activeElem = -1;
        public float total;

        public float CalculateTotal(IList<CartViewModel> cartList)
        {
            float total = 0;
            foreach (var elem in cartList)
            {
                total += elem.Article.Price * elem.Amount;
            }

            return total;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            this.Category = await _context.Category.ToListAsync();
            this._cartCookieDict = this.GetCookie("cart");
            this.Cart = new List<CartViewModel>();

            if (id == null)
            {
                this.activeElem = -1;

                foreach (var cookie in this._cartCookieDict)
                {
                    var article_found = await _context.Article.Where(article => article.Id == cookie.Key).SingleOrDefaultAsync();
                    if (article_found != null)
                        this.Cart.Add(new CartViewModel(article_found, cookie.Value));
                    else
                    {
                        this._cartCookieDict.Remove(cookie.Key);
                        SetCookie(this._cartCookieDict);
                    }

                }

                this.total = CalculateTotal(this.Cart);

                return Page();
            }

            foreach (var cookie in this._cartCookieDict)
            {
                var article_found = await _context.Article.Where(article => article.Id == cookie.Key && article.CategoryId == id).SingleOrDefaultAsync();
                if (article_found != null)
                    this.Cart.Add(new CartViewModel(article_found, cookie.Value));

            }

            this.activeElem = id;

            this.total = CalculateTotal(this.Cart);

            return Page();
        }
        public IActionResult OnPostAppendToCart()
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

        public IActionResult OnPostRemoveFromCart()
        {
            var articleId = int.Parse(Request.Form["articleId"]);
            this._cartCookieDict = this.GetCookie("cart");

            if (this._cartCookieDict.ContainsKey(articleId))
            {
                this._cartCookieDict[articleId] -= 1;
                if (this._cartCookieDict[articleId] <= 0)
                    this._cartCookieDict.Remove(articleId);
                SetCookie(this._cartCookieDict);
            }

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
