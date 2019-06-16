using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Szertar.Dal.Managers.Interfaces;

namespace Szertar.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class UserController : Controller
	{
		private readonly IUserManager _userManager;
		private readonly UserManager<Szertar.Dal.Entities.ApplicationUser> _user;


		public UserController(IUserManager userManager, UserManager<Szertar.Dal.Entities.ApplicationUser> user)
		{
			_userManager = userManager;
			_user = user;
		}

		public IActionResult ListUsers()
		{

			return View(_userManager.GetAllUser());
		}

		public IActionResult GiveMod(string id) {
			var result = _user.AddToRoleAsync(_user.FindByIdAsync(id).Result, "Administrator").Result;
			return RedirectToAction(nameof(ListUsers));
		}
	}
}