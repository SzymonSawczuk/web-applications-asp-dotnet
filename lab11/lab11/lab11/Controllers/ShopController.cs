using lab10.Data;
using lab10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using lab11.Models;
using System.Linq.Expressions;
using LinqKit;

namespace lab10.Controllers
{
    public class ShopController : Controller
    {
        private readonly MyDbContext _context;

        public ShopController(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.activeElem = -1;
            ViewBag.articles = await _context.Article.ToListAsync();
            HttpContext.Session.SetInt32("activeElem", -1);

            return View(await _context.Category.ToListAsync());
        }

        [HttpGet("/Shop/Index/{categoryId}"), ActionName("Index")]
        public async Task<IActionResult> Index(int categoryId)
        {
            ViewBag.activeElem = categoryId;
            HttpContext.Session.SetInt32("activeElem", categoryId);
            ViewBag.articles = await _context.Article.Where(article => article.CategoryId == categoryId).ToListAsync();
            return View(await _context.Category.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.backId = HttpContext.Session.GetInt32("activeElem");
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        private Dictionary<int, int> _cartCookieDict;

        public float CalculateTotal(List<CartViewModel> cartList)
        {
            float total = 0;
            foreach (var elem in cartList)
            {
                total += elem.Article.Price * elem.Amount;
            }

            return total;
        }

        public async Task<IActionResult> Cart()
        {
            this._cartCookieDict = this.GetCookie("cart");
            ViewBag.activeElem = -1;
            

            List<CartViewModel> cartList = new List<CartViewModel>();
            var articles = await _context.Article.ToListAsync();

            foreach (Article article in articles)
            {
                if (this._cartCookieDict.ContainsKey(article.Id)) 
                    cartList.Add(new CartViewModel(article, this._cartCookieDict[article.Id]));
                    
            }

            ViewBag.categories = await _context.Category.ToListAsync();

            ViewBag.total = CalculateTotal(cartList);

            return View(cartList);
        }

        [HttpGet("/Shop/Cart/{categoryId}"), ActionName("Cart")]
        public async Task<IActionResult> Cart(int categoryId)
        {

            this._cartCookieDict = this.GetCookie("cart");
            ViewBag.activeElem = categoryId;


            List<CartViewModel> cartList = new List<CartViewModel>();
            var articles = await _context.Article.Where(article => article.CategoryId == categoryId).ToListAsync();

            foreach (Article article in articles)
            {
                if (this._cartCookieDict.ContainsKey(article.Id))
                    cartList.Add(new CartViewModel(article, this._cartCookieDict[article.Id]));

            }

            ViewBag.categories = await _context.Category.ToListAsync();

            ViewBag.total = CalculateTotal(cartList);

            return View(cartList);
        }

        [HttpPost]
        public IActionResult AppendToCart(int articleId)
        {
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

        [HttpPost]
        public IActionResult RemoveFromCart(int articleId)
        {
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
