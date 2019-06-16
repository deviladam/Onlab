using System;
using System.Collections.Generic;
using System.Text;
using Szertar.Dal.Dto;
using Szertar.Dal.Entities;

namespace Szertar.Dal.Managers.Interfaces
{
	public interface IOrderManager
	{
		int TakeCartOrder(OrderTimes order, string userId);

		List<OrderDetails> GetOrders(string userId);

		int DeleteOrder(int orderId, string userId);

		string Up(int orderId);
		string Down(int orderId);
	}
}
