using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Szertar.Dal.Dto;
using Szertar.Dal.Managers.Interfaces;

namespace Szertar.Controllers
{
    public class OrderController : Controller
    {
		private readonly IOrderManager _orderManager;

		public OrderController(IOrderManager orderManager)
		{
			_orderManager = orderManager;
		}

		[Authorize]
		public IActionResult ListOrders()
		{
			var userId = User.Claims.FirstOrDefault((c => c.Type == ClaimTypes.NameIdentifier)).Value;
			var orders = _orderManager.GetOrders(userId);
			return View( orders );
		}

		[Authorize]
		[Authorize(Roles = "Administrator")]
		public IActionResult ListOrdersForUser(string id)
		{
			var orders = _orderManager.GetOrders(id);
			return View(orders);
		}

		[HttpGet]
		[Authorize]
		public IActionResult OrderAdd()
        {
            return View();
        }

		[HttpPost]
		[Authorize]
		public IActionResult OrderAdd(OrderTimes order)
		{
			if (order.ReleaseTime < DateTime.Now) {
				ModelState.AddModelError(string.Empty, "Túl korai az átvétel időpontja.");
				return View();
			}

			if (order.ReleaseTime > order.Deadline)
			{
				ModelState.AddModelError(string.Empty, "Visszahozatal időpontja nem valós.");
				return View();
			}
			var userId = User.Claims.FirstOrDefault((c => c.Type == ClaimTypes.NameIdentifier)).Value;
			_orderManager.TakeCartOrder(order,userId);
			return RedirectToAction(nameof(ListOrders));
		}

		[HttpPost]
		[Authorize(Roles = "Administrator")]
		[ValidateAntiForgeryToken]
		public IActionResult Up(int orderId)
		{
			string userId = _orderManager.Up(orderId);

			return RedirectToAction(nameof(ListOrdersForUser), new { id = userId });
		}

		[HttpPost]
		[Authorize(Roles = "Administrator")]
		[ValidateAntiForgeryToken]
		public IActionResult Down(int orderId)
		{
			string userId = _orderManager.Down(orderId);
			return RedirectToAction(nameof(ListOrdersForUser), new {id = userId } );
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int orderId)
		{
			var userId = User.Claims.FirstOrDefault((c => c.Type == ClaimTypes.NameIdentifier)).Value;
			int result = _orderManager.DeleteOrder(orderId, userId);
			if (result != 0)
			{
				TempData["error"] = "Törlés nem sikerült! (Csak függőben lévő rendelést lehet törölni.)";
			}
			return RedirectToAction(nameof(ListOrders));
		}
	}
}