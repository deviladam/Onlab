using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Szertar.Dal.Managers;
using Szertar.Models;

namespace Szertar.Controllers
{
	public class HomeController : Controller
	{
		private IItemManager _itemManager;

		public HomeController(IItemManager itemManager)
		{
			_itemManager = itemManager;
		}
		
		public IActionResult Index()
		{
			// TODO: DI segítségével hogyan tudod elérni.
			
			var items = _itemManager.GetAllItems();

			return View( items );
		}

		[HttpPost]
		public IActionResult AddToCart( int itemId, int count )
		{
			// Kliens oldal ajax-szal hívni. Pl Unobtrustiv ajax plugin behúzása
			// TODO: implement. Figyelni, ha ez az Id már van a kosárban, akkor csak növelni.
			throw new NotImplementedException();
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
