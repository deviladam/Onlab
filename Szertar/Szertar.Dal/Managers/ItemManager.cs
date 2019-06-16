using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Szertar.Dal.Dto;
using Szertar.Dal.Entities;

namespace Szertar.Dal.Managers
{
	public class ItemManager : IItemManager
	{
		
		private readonly ApplicationDbContext _dbContext = null;

		public ItemManager(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void AddItem(ItemDetails item)
		{
			_dbContext.Add(item);
			_dbContext.SaveChanges();
		}

		public List<ItemHeader> GetAllItems(string[] filters)
		{
			var items = _dbContext.Items.Where(i => i.Activity.Contains(!String.IsNullOrEmpty(filters[2])?filters[2]:"")).Select(i => new ItemHeader
			{
				Id = i.Id,
				Name = i.Name,
				Price = i.Price,
				Stock = i.Stock,
				Image = i.Image,
				AvailableCount = i.AvailableCount,
				Description = i.Description
			});

			if (!String.IsNullOrEmpty(filters[0]))
			{
				items = items.Where(i => i.Name.Contains(filters[0]));
			}

			if (filters[1] == "on")
			{
				items = items.Where(i => i.AvailableCount > 0);
			}

			if (filters[3] == "on")
			{
				string[] split = filters[4].Split(",");
				int min = int.Parse(split[0]);
				int max = int.Parse(split[1]);
				items = items.Where(i => i.Price >= min && i.Price <= max);
			}


			var result = items.OrderBy(i => i.Name).ToList();
			return result;

		}

	}
}
