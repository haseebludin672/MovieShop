using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
      public IActionResult Register()
        {
            return View();
        }

    
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            // save the password and account info with salt
            if (ModelState.IsValid)
            {
                // save the database
                var user = await _accountService.CreateUser(model);
                return RedirectToAction("Login");
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View();
            var userLogedIn = await _accountService.ValidateUser(model.Email, model.Password);
            if (userLogedIn != null)
            {
                // create an authentication cookie and store some claims information in the cookie
                // user related information
                // Driving Licence
                // First Name, Last Name, Date Of Birth, Location

                // create claims object to store user claims information

                var claims = new List<Claim>
            {
                new(ClaimTypes.Email, userLogedIn.Email),
                new(ClaimTypes.NameIdentifier, userLogedIn.Id.ToString()),
                new(ClaimTypes.GivenName, userLogedIn.FirstName),
                new(ClaimTypes.Surname, userLogedIn.LastName),
                new(ClaimTypes.DateOfBirth, userLogedIn.DateOfBirth.ToShortDateString()),
                new("FullName", userLogedIn.FirstName + "," + userLogedIn.LastName),
                new("Language", "en")
            };

                //Identity object
                var cliamsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //create the cookie
                //SingInAsync
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                                                new ClaimsPrincipal(cliamsIdentity));
                return LocalRedirect("~/");
            }
            else
            {
                return View();  
            }
            
            
        }
    }
}
