using System;
using System.Collections.Generic;
using System.Text;

namespace Szertar.Dal.Dto
{
	public class CartItemHeader
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public int AvailableCount { get; set; }
		public int Price { get; set; }
		public byte[] Image { get; set; }
	}
}
