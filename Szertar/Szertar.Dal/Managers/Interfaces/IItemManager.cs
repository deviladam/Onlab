using System;
using System.Collections.Generic;
using System.Text;
using Szertar.Dal.Dto;

namespace Szertar.Dal.Managers
{
	public interface IItemManager
	{
		List<ItemHeader> GetAllItems();
	}
}
