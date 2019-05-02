using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Szertar.Dal.Entities
{
	public class Cart
	{
		public int CartId { get; set; }

		[ForeignKey("ApplicationUser")]
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public virtual List<CartItem> Items { get; set; }
	}
}
