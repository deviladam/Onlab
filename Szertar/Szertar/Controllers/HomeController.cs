using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Szertar.Dal.Dto;
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
		
		public IActionResult Index(string name, string have, string type, string priceDo, string price, int? p)
		{
			var pageIndex = p ?? 1;

			string[] filters = { name, have, type, priceDo, price };
			ViewBag.filters = filters;
			var items = _itemManager.GetAllItems(filters, pageIndex);

			ViewBag.OnePageOfProducts = items;
			return View(items);
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public IActionResult ItemAdd()
		{
			return View();
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public async Task<IActionResult> ItemAdd(ItemDetails item, List<IFormFile> Image)
		{
			foreach (var i in Image)
			{
				if (i.Length > 0)
				{
					using (var stream = new MemoryStream())
					{
						await i.CopyToAsync(stream);
						item.Image = stream.ToArray();
					}
				}
			}
			item.AvailableCount = item.Stock;
			_itemManager.AddItem(item);
			return RedirectToAction(nameof(Index));
		}




		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
