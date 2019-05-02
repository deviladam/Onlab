using System;
using System.Collections.Generic;
using System.Text;
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

		public void TakeCartOrder()
		{
			throw new NotImplementedException();
		}
	}
}
