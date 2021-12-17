using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCPerfectCars.Models;
using MVCPerfectCars.Service;
using MVCPerfectCarsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPerfectCars.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly SignInManager<User> signInManager;

        public AccountController(
            IAccountService accountService,
            SignInManager<User> signInManager
            )
        {
            this.accountService = accountService;
            this.signInManager = signInManager;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await accountService.LoginAsync(model);
            if (result.Succeeded)
            {
                return Redirect("/admin");
            }
            else
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı girişi");
                return View(model);
            }
                     


        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
