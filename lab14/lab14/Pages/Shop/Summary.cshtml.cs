using lab14.Model;
using lab14.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab14.Pages.Shop
{
    [Authorize(Policy = "NotAdmin")]
    [Authorize]
    public class SummaryModel : PageModel
    {

        private readonly lab14.Data.MyDbContext _context;
        private Dictionary<int, int> _cartCookieDict;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SummaryModel(lab14.Data.MyDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public IList<CartViewModel> Cart { get; set; }

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

        public SelectList SelectList { get; set; }
        [BindProperty]
        public string Selected { get; set; }

        public async Task<IActionResult> OnGet()
        {
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
                    this.Cart.Add(new CartViewModel(article_found, cookie.Value));

            }

            this.total = CalculateTotal(this.Cart);

            if (this.total == 0)
                return RedirectToPage("Shop");

            List<String> paymentOptions = new() { "BLIK", "Cash on delivery", "Card" };

            SelectList = new SelectList(paymentOptions);

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
            [Required]
            public string PhoneNumber { get; set; }

            [Display(Name = "First Name")]
            [Required]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            [Required]
            public string LastName { get; set; }

            [Display(Name = "Address")]
            [Required]
            public string Address { get; set; }

            [Display(Name = "Post Code")]
            [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "{0} must be for example 12-345.")]
            [Required]
            public string? PostCode { get; set; }

            [Display(Name = "City")]
            [Required]
            public string City { get; set; }

            [Display(Name = "Country")]
            [Required]
            public string Country { get; set; }

            [Display(Name = "Voivodeship")]
            [Required]
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

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnPostReceiptAsync()
        {
            HttpContext.Session.SetString("Payment", Selected);

            this._cartCookieDict = this.GetCookie("cart");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            if (Input.Address != user.Address)
            {
                user.Address = Input.Address;
            }

            if (Input.PostCode != user.PostCode)
            {
                user.PostCode = Input.PostCode;
            }

            if (Input.City != user.City)
            {
                user.City = Input.City;
            }

            if (Input.Country != user.Country)
            {
                user.Country = Input.Country;
            }

            if (Input.Voivodeship != user.Voivodeship)
            {
                user.Voivodeship = Input.Voivodeship;
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);

            return RedirectToPage("Receipt");
        }

        public void SetCookie(Dictionary<int, int> cartCookieDict)
        {

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("cart", JsonConvert.SerializeObject(cartCookieDict), option);
        }

    }
}
