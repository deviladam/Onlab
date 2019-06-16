using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szertar.Dal.Dto;
using Szertar.Dal.Managers.Interfaces;

namespace Szertar.Dal.Managers
{
	public class UserManager : IUserManager
	{
		private readonly ApplicationDbContext _dbContext = null;

		public UserManager(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<UserDetails> GetAllUser()
		{
			var users = _dbContext.AppUsers.Select(u => new UserDetails
			{
				Name = u.UserName,
				Email = u.Email,
				UserID = u.Id
			}).OrderBy(u => u.Name).ToList();
			return users;
		}
	}
}
