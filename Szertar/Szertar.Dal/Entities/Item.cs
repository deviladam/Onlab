using System;
using System.Collections.Generic;
using System.Text;

namespace Szertar.Dal.Entities
{
	public class Item
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Stock { get; set; }
		public int AvailableCount { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
		public string Activity { get; set; }
		public byte[] Image { get; set; }
	}
}
