using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Szertar.Dal.Entities
{
	public class ApplicationUser : IdentityUser
	{

		public virtual List<Order> Orders { get; set; }
		
		public virtual Cart Cart { get; set; }
	}
}
