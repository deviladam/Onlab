using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Szertar.Dal.Entities;

namespace Szertar.Dal.Dto
{
	public class UserDetails
	{
		[Display(Name = "Felhasználónév")]
		public string Name { get; set; }
		[Display(Name = "E-mail")]
		public string Email { get; set; }
		public string UserID { get; set; }
	}
}
