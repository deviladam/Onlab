using System;
using System.Collections.Generic;
using System.Text;

namespace Szertar.Dal.Entities
{
	public class CartItem
	{
		public int Id { get; set; }
		public int ItemId { get; set; }
		public virtual Item Item { get; set; }
		public int Quantity { get; set; }
	}
}
