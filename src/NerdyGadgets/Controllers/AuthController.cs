using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NerdyGadgets.Data;
using NerdyGadgets.Dtos;
using NerdyGadgets.Models;
using NerdyGadgets.Services;
using System.Security.Claims;

namespace NerdyGadgets.Controllers
{
    public class AuthController : Controller
    {
        private AuthService _authService;
        private ApplicationDbContext _context;

        public AuthController(AuthService service, ApplicationDbContext context)
        {
            this._authService = service;
            this._context = context;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthDto auth)
        {
            
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(String.Empty, "Not all required fields were filled.");
                return View(auth);
            }

            if (this._authService.CheckLogin(auth, out User? user) && user != null)
            {
                // Make a few claims.
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                // Set the authentication in the cookies.
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties()
                {
                    ExpiresUtc = DateTime.UtcNow.AddDays(28),
                    IsPersistent = true
                };

                // 'Login'.
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Email or password were incorrect.");
                return View(auth);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (ModelState.IsValid)
            {
                bool valid = true;
                if (!await _authService.EmailInUse(dto.EmailAddress))
                {
                    ModelState.AddModelError("EmailAddress", "Email address is already in use");
                    valid = false;
                }
                if (dto.Password != dto.PasswordConfirmation)
                {
                    ModelState.AddModelError("Password", "Passwords are not equal");
                    valid = false;
                }

                if (!valid)
                {
                    ModelState.AddModelError(String.Empty, "");
                    return View(dto);
                }

                // Create new address and user.
                var address = new Address
                {
                    Id = Guid.NewGuid()
                };

                var user = new User
                {
                    Password = AuthService.Hash(dto.Password),
                    Email = dto.EmailAddress,
                    FirstName = dto.FirstName,
                    Preposition = dto.Preposition,
                    LastName = dto.LastName,
                    AddressId = address.Id,
                    AccountCreatedAt = DateTime.UtcNow,
                    Role = UserRole.Unconfirmed
                };

                this._context.Addresses.Add(address);
                this._context.Users.Add(user);

                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Auth");
            }

            ModelState.AddModelError(String.Empty, "Not all required fields were filled");
            return View(dto);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}