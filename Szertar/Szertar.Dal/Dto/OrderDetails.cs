using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Szertar.Dal.Entities;

namespace Szertar.Dal.Dto
{
	public class OrderDetails : OrderTimes
	{
		[Display(Name = "Státusz")]
		public int Status { get; set; }
		[Display(Name = "Ár (Ft)")]
		public int Price { get; set; }
		public int OrderId { get; set; }
		public List<OrderdItem> Items { get; set; }
	}
}
