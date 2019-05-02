using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szertar.Dal.Entities;
using Szertar.Dal.Managers.Interfaces;
using Microsoft.AspNetCore.Identity;
using Szertar.Dal.Dto;

namespace Szertar.Dal.Managers
{
	public class CartManager : ICartManager
	{
		private readonly ApplicationDbContext _dbContext = null;
		

		public CartManager(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void AddItemToCart(int itemID, int quantity, string userId)
		{
			var itemQuantity = _dbContext.Items.Where(i => i.Id == itemID).SingleOrDefault().AvailableCount;
			if (quantity > itemQuantity || quantity < 0) return;

			var cart = _dbContext.Carts.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
			if (cart == null)
			{
				cart = new Cart
				{
					ApplicationUserId = userId
				};
				_dbContext.Add(cart);
			}

			var item = _dbContext.CartItems.Where(i => i.ItemId == itemID && i.CartId == cart.CartId).SingleOrDefault();

			if (item == null) {
				var cartItem = new CartItem
				{
					ItemId = itemID,
					Quantity = quantity,
					CartId = cart.CartId 
				};
				_dbContext.Add(cartItem);
			} else
			{
				item.Quantity += quantity;
			}

			_dbContext.Items.Where(i => i.Id == itemID).Single().AvailableCount -= quantity; 
			_dbContext.SaveChanges();
		}

		public void DeleteCartItem(int cartItemId, string userId)
		{
			var cart = _dbContext.Carts.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
			var cartItem = _dbContext.CartItems.Where(ci => ci.Id == cartItemId).SingleOrDefault();
			if (cart.CartId != cartItem.CartId) return;

			var item = _dbContext.Items.Where(i => i.Id == cartItem.ItemId).SingleOrDefault();
			item.AvailableCount += cartItem.Quantity;
			_dbContext.Remove(cartItem);
			_dbContext.SaveChanges();
		}

		public List<CartItemHeader> GetCurrentCartItemsList(string userId)
		{
			var cartItems = _dbContext.CartItems.Where(ci => ci.CartId == _dbContext.Carts.Where(c => c.ApplicationUserId == userId).SingleOrDefault().CartId)
				.Select(ci => new CartItemHeader
				{
					Id = ci.Id,
					Name = ci.Item.Name,
					AvailableCount = ci.Item.AvailableCount,
					Price = ci.Item.Price,
					Quantity = ci.Quantity
				}).OrderBy(ci => ci.Name).ToList();
			return cartItems;
		}

		public int GetItemsCountOfCart(string userId)
		{
			var cart = _dbContext.Carts.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
			if (cart == null) return 0;
			return _dbContext.CartItems.Where(i => i.CartId == cart.CartId ).Count();
		}
	}
}
