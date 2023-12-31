﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Practical_17.Enums;
using Practical_17.Interfaces;
using Practical_17.Models;
using Practical_17.ViewDataModel;
using System.Security.Claims;

namespace Practical_17.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserRepository _repository;

		public AccountController(IUserRepository repository)
		{
			_repository = repository;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RegisterAsync(RegisterViewModel user)
		{
			if (ModelState.IsValid)
			{
				var temp = await _repository.GetUserByEmailAsync(user.Email);
				if (temp == null)
				{

					await _repository.RegisterUserAsync(user);
					return RedirectToAction(nameof(Login));
				}
				else
				{
					ModelState.AddModelError("", "User Already Registered!");
					return View(user);
				}
			}
			return View(user);
		}


		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LoginAsync(LoginViewModel model)
		{
			if (model == null)
			{
				return NotFound();
			}
			UserLoginStatus status = await _repository.LoginUserAsync(model);

			switch (status)
			{
				case UserLoginStatus.UserNull:
					return NotFound();
					break;
				case UserLoginStatus.UserNotFound:
					ModelState.AddModelError("", "No such Account found!");
					return View(model);
					break;

				case UserLoginStatus.InvalidPassword:
					ModelState.AddModelError("", "Invalid User Id and Password");
					return View(model);
					break;

				case UserLoginStatus.LoginSuccess:
					await LoginUserAsync(model);

					return RedirectToAction("Index", "Home");
					break;
			}


			return RedirectToAction("Index", "Home");
		}

		private async Task LoginUserAsync(LoginViewModel model)
		{
			User user = await _repository.GetUserByEmailAsync(model.Email);
			List<string> roles = _repository.GetUserRoles(user.Id);

			List<Claim> claims = new List<Claim>();

			claims.Add(new Claim(ClaimTypes.Name, model.Email));
			foreach (var item in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, item));
			}

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrinciple);
			HttpContext.Session.SetString("Email", user.Email);

		}


		public async Task<IActionResult> Logout()
		{
			var cookieKeys = Request.Cookies.Keys;
			foreach (var cookieKey in cookieKeys)
			{
				Response.Cookies.Delete(cookieKey);
			}
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}

		public IActionResult AccessDenied()
		{
			return View();
		}

	}
}
