using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Szertar.Dal;
using Szertar.Dal.Entities;
using Szertar.Dal.Managers.Interfaces;

namespace Szertar.Controllers
{
    public class CartController : Controller
    {

		private readonly ICartManager _cartManager;

		public CartController(ICartManager cartManager)
		{
			_cartManager = cartManager;
		}

		[HttpPost]
		[Authorize]
		public IActionResult AddToCart(int itemId, int count)
		{
			if (count == 0)
			{
				TempData["error"] = "Nincsen megadva a eszközök darabszáma!";
				return RedirectToAction("Index", "Home");
			}
				

			// TODO: Calimek közül kiszedni. Ha jó akkor a ClaimsPrincipalhoz egy extension method / Property UserId.
			var a = User.Claims.FirstOrDefault((c => c.Type == ClaimTypes.NameIdentifier)).Value;

			var userId = User.Claims.FirstOrDefault((c => c.Type == ClaimTypes.NameIdentifier)).Value;

			if (_cartManager.AddItemToCart(itemId, count, userId) == 1)
			{
				TempData["error"] = "Nincsen rendben a eszközök darabszáma!";
			}
			ViewData["CartItemsNumber"] = _cartManager.GetItemsCountOfCart(userId);
 			return RedirectToAction("Index","Home");
		}

		[Authorize]
		public IActionResult Cart()
		{
			var userId = User.Claims.FirstOrDefault((c => c.Type == ClaimTypes.NameIdentifier)).Value;
			var cartItemsList = _cartManager.GetCurrentCartItemsList(userId);
			int orderPrice = 0;
			foreach (var item in cartItemsList)
			{
				orderPrice += item.Quantity * item.Price;
			}
			ViewData["orderPrice"] = orderPrice.ToString();
			ViewData["CartItemsNumber"] = _cartManager.GetItemsCountOfCart(userId);
			return View(cartItemsList);
		}


		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public  IActionResult Delete(int itemId)
		{
			var userId = User.Claims.FirstOrDefault((c => c.Type == ClaimTypes.NameIdentifier)).Value;
			_cartManager.DeleteCartItem(itemId, userId);
			return RedirectToAction(nameof(Cart));
		}
	}
}
