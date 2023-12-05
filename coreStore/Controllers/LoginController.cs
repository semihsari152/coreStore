using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace coreStore.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        public async Task<IActionResult> Login(Admin admin, bool remember = false)
        {
            // Kullanıcı zaten giriş yapmışsa, ana sayfaya yönlendir
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            Context c = new Context();
            var datavalue = c.Admins.FirstOrDefault(x => x.Username == admin.Username && x.Password == admin.Password);

            if (datavalue != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, admin.Username)
        };
                var userIdentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                AuthenticationProperties authenticationProperties = null;

                if (remember)
                {
                    // Eğer "Remember Me" seçeneği işaretlendiyse, çerezin süresini 1 gün olarak ayarla
                    authenticationProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(1)
                    };
                }

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

    }
}
