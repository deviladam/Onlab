using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szertar.Dal.Dto;
using Szertar.Dal.Entities;
using Szertar.Dal.Managers.Interfaces;

namespace Szertar.Dal.Managers
{
	public class OrderManager : IOrderManager
	{
		private readonly ApplicationDbContext _dbContext = null;

		public OrderManager(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public int DeleteOrder(int orderId, string userId)
		{
			var order = _dbContext.Orders.Where(o => o.Id == orderId).SingleOrDefault();
			if (order == null){return 1;}
			if (order.ApplicationUserId != userId) { return 1; }
			if (order.Status != 0) { return 1; }
			_dbContext.Remove(order);
			foreach (var orderItem in order.Items)
			{
				orderItem.Item.AvailableCount += orderItem.Quantity;
				_dbContext.Remove(orderItem);
			}
			_dbContext.SaveChanges();
			return 0;
		}

		

		public List<OrderDetails> GetOrders(string userId)
		{
			var orders = _dbContext.Orders.Where(o => o.ApplicationUserId == userId).Select(o => new OrderDetails {
				Status = o.Status,
				Price = o.Price,
				Items = o.Items,
				ReleaseTime = o.ReleaseTime,
				Deadline = o.Deadline,
				OrderId = o.Id
			}).ToList();
			return orders;
		}

		public int TakeCartOrder(OrderTimes orderDetails, string userId)
		{
			var cart = _dbContext.Carts.Where(c => c.ApplicationUserId == userId).SingleOrDefault();
			if (cart == null) return 1;
			int price = 0;
			Order order = new Order
			{
				ReleaseTime = orderDetails.ReleaseTime,
				Deadline = orderDetails.Deadline,
				Status = 0,
				ApplicationUserId = userId
			};
			var dbOrder = _dbContext.Add(order).Entity;
			foreach (var item in cart.Items)
			{
				OrderdItem orderdItem = new OrderdItem
				{
					Quantity = item.Quantity,
					ItemId = item.ItemId,
					OrderId = order.Id
				};
				price += item.Quantity * item.Item.Price;
				_dbContext.Add(orderdItem);
				_dbContext.Remove(item);
			}
			_dbContext.Remove(cart);
			TimeSpan substract = orderDetails.Deadline.Subtract(orderDetails.ReleaseTime);
			dbOrder.Price =  (substract.Days + (substract.Minutes>0?1:0) ) * price;
			_dbContext.SaveChanges();
			return 0;
		}

		public string Up(int orderId)
		{
			var order = _dbContext.Orders.Where(o => o.Id == orderId).SingleOrDefault();
			if (order.Status < 3 && order.Status > -1)
			{
				if ( order.Status == 2)
				{
					foreach (var orderItem in order.Items)
					{
						orderItem.Item.AvailableCount += orderItem.Quantity;
					}
				}
				order.Status++;
			}

			_dbContext.SaveChanges();
			return order.ApplicationUserId;
		}

		public string Down(int orderId)
		{
			var order = _dbContext.Orders.Where(o => o.Id == orderId).SingleOrDefault();
			if (order.Status > -1)
			{
				if (order.Status == 0 )
				{
					foreach (var orderItem in order.Items)
					{
						orderItem.Item.AvailableCount += orderItem.Quantity;
					}	
				}
				if (order.Status == 3)
				{
					foreach (var orderItem in order.Items)
					{
						orderItem.Item.AvailableCount -= orderItem.Quantity;
					}
				}

				order.Status--;
			}

			_dbContext.SaveChanges();
			return order.ApplicationUserId;
		}

		public int GetCountForType(int type, string userId)
		{
			var count = _dbContext.Orders.Where(c => c.ApplicationUserId == userId).Where(o => o.Status == type).Count();
			return count;
		}
	}
}
