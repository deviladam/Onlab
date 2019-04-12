using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Szertar.Dal.Dto;

namespace Szertar.Dal.Managers
{
	public class ItemManager : IItemManager
	{
		// TODO: DI-al betölteni a db contextet.

		//public static ApplicationDbContext dbcontext = null;

		//public static void InitDbcontext(IServiceProvider serviceProvider)
		//{
		//	IServiceScope scope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope();
		//	dbcontext = scope.ServiceProvider.GetService<ApplicationDbContext>();
		//}

		private readonly ApplicationDbContext _dbContext = null;

		public ItemManager(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<ItemHeader> GetAllItems()
		{
			var items = _dbContext.Items.Select(i => new ItemHeader {
				Id = i.Id,
				Name = i.Name,
				Price = i.Price,
				Stock = i.Stock
			}).ToList();

			//var items = from t in dbcontext.Items
						//select t;
			return items;

		}

	}
}
