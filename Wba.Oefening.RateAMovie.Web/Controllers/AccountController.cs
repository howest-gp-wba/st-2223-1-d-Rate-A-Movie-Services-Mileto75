using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.Oefening.RateAMovie.Web.Data;
using Wba.Oefening.RateAMovie.Core.Entities;
using Wba.Oefening.RateAMovie.Web.ViewModels;
using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;
using Wba.Oefening.RateAMovie.Web.Services.Interfaces;

namespace Wba.Oefening.RateAMovie.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly MovieContext _movieContext;
        private readonly IAccountService _accountService;
        

        public AccountController(MovieContext movieContext,
            IAccountService accountService)
        {
            _movieContext = movieContext;
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterViewModel accountRegisterViewModel)
        {
            //Register user using service class
            if(!await _accountService.register(accountRegisterViewModel.Username,accountRegisterViewModel.Password,accountRegisterViewModel.Firstname,accountRegisterViewModel.Lastname))
            {
                ModelState.AddModelError("", "Credentials seem to exist in database. Would you like to request a password reset?");
            }
            if(!ModelState.IsValid)
            {
                return View(accountRegisterViewModel);
            }
            return RedirectToAction("Registered");
        }

        [HttpGet]
        public IActionResult Login()
        {
            //get the username from the cookie
            //create viewmodel => put username in model

            var accountLoginViewModel = new AccountLoginViewModel
            {
                Username = Request.Cookies["username"]
            };
            return View(accountLoginViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginViewModel accountLoginViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(accountLoginViewModel);
            }
            
            if (!await _accountService.Login(accountLoginViewModel.Username, accountLoginViewModel.Password))
            {
                ModelState.AddModelError("", "Please provide correct credentials!");
                return View(accountLoginViewModel);
            }
            //Logged in
            //create the cookie
            Response.Cookies.Append("username",accountLoginViewModel.Username,
                new CookieOptions 
                {
                    Expires = DateTime.Now.AddDays(2)
                });
            //set a session var LoggedIn = 1
            HttpContext.Session.SetInt32("loggedIn", 1);
            HttpContext.Session.SetString("username", accountLoginViewModel.Username);
            return RedirectToAction("Index", "Movies");
        }

        [HttpGet]
        public IActionResult Registered()
        {
            return View();
        }
    }
}
