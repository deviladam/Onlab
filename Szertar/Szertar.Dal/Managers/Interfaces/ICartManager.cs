using System;
using System.Collections.Generic;
using System.Text;
using Szertar.Dal.Dto;

namespace Szertar.Dal.Managers.Interfaces
{
	public interface ICartManager
	{
		void AddItemToCart(int itemID, int quantity, string userId);
		int GetItemsCountOfCart(string userId);
		List<CartItemHeader> GetCurrentCartItemsList(string userId);

		void DeleteCartItem(int CartItem, string userId);

	}
}
