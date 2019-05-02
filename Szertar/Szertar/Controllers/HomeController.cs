using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Szertar.Dal.Managers;
using Szertar.Models;

namespace Szertar.Controllers
{
	public class HomeController : Controller
	{
		private IItemManager _itemManager;
		private readonly IAuthorizationService _authorizationService;

		public HomeController(IAuthorizationService authorizationService, IItemManager itemManager)
		{
			_itemManager = itemManager;
			_authorizationService = authorizationService;
		}
		
		public IActionResult Index()
		{
			// TODO: DI segítségével hogyan tudod elérni.
			
			var items = _itemManager.GetAllItems();

			return View( items );
		}



		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
