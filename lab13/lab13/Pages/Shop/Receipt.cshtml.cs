using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using lab13.Model;
using lab13.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace lab13.Pages.Shop
{
    [Authorize(Policy = "NotAdmin")]
    [Authorize]
    public class ReceiptModel : PageModel
    {

        private readonly lab13.Data.MyDbContext _context;
        private Dictionary<int, int> _cartCookieDict;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ReceiptModel(lab13.Data.MyDbContext context, UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

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

        [BindProperty]
        public IList<CartViewModel> Cart { get; set; }

        [TempData]
        public string SelectedPayment { get; set; }

        public async Task<IActionResult> OnGet()
        {
            SelectedPayment = HttpContext.Session.GetString("Payment");

            if(SelectedPayment == null || SelectedPayment == "")
                return RedirectToPage("Shop");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            this._cartCookieDict = this.GetCookie("cart");
            this.Cart = new List<CartViewModel>();

            foreach (var cookie in this._cartCookieDict)
            {
                var article_found = await _context.Article.Where(article => article.Id == cookie.Key).SingleOrDefaultAsync();
                if (article_found != null)
                {
                    this.Cart.Add(new CartViewModel(article_found, cookie.Value));
                    this._cartCookieDict.Remove(cookie.Key);
                    SetCookie(this._cartCookieDict);
                }

            }

            this.total = CalculateTotal(this.Cart);

            if (this.total == 0)
                return RedirectToPage("Shop");

            HttpContext.Session.SetString("Payment", "");

            Response.Cookies.Delete("cart");

            return Page();

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

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Address")]
            public string Address { get; set; }

            [Display(Name = "Post Code")]
            [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "{0} must be for example 12-345.")]
            public string? PostCode { get; set; }

            [Display(Name = "City")]
            public string City { get; set; }

            [Display(Name = "Country")]
            public string Country { get; set; }

            [Display(Name = "Voivodeship")]
            public string Voivodeship { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                PostCode = user.PostCode,
                City = user.City,
                Country = user.Country,
                Voivodeship = user.Voivodeship
            };
        }

       
        public void SetCookie(Dictionary<int, int> cartCookieDict)
        {

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("cart", JsonConvert.SerializeObject(cartCookieDict), option);
        }
    }
}
