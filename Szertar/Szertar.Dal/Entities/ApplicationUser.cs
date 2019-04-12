using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Szertar.Dal.Entities
{
	public class ApplicationUser : IdentityUser
	{
		// TODO: Egyedi tulajdonságok
		public virtual List<Order> Orders { get; set; }
		
		public virtual Cart Cart { get; set; }

	}
}
