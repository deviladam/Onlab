using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Szertar.Dal.Dto
{
	public class OrderTimes
	{
		[Required(ErrorMessage = "A(z) {0} kitöltése kötelező")]
		[Display(Name = "Átvétel ideje")]
		public DateTime ReleaseTime { get; set; }
		[Required(ErrorMessage = "A(z) {0} kitöltése kötelező")]
		[Display(Name = "Visszahozatal")]
		public DateTime Deadline { get; set; }
	}
}
