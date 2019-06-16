using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Szertar.Dal.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public DateTime ReleaseTime { get; set; }
		public DateTime Deadline { get; set; }
		public int Status { get; set; }
		public int Price { get; set; }

		[ForeignKey("ApplicationUser")]
		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public virtual List<OrderdItem> Items { get; set; }
	}
}
