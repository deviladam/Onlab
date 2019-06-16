using System;
using System.Collections.Generic;
using System.Text;
using Szertar.Dal.Dto;

namespace Szertar.Dal.Managers.Interfaces
{
	public interface IUserManager
	{
		List<UserDetails> GetAllUser();
	}
}
