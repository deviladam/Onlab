using X.PagedList;
using System;
using System.Collections.Generic;
using System.Text;
using Szertar.Dal.Dto;


namespace Szertar.Dal.Managers
{
	public interface IItemManager
	{
		X.PagedList.IPagedList<ItemHeader> GetAllItems(string[] filters, int? page);
		void AddItem(ItemDetails item);
	}
}
