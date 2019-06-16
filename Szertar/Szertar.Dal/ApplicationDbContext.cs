using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Szertar.Dal.Dto;
using Szertar.Dal.Entities;

namespace Szertar.Dal
{
	public class ApplicationDbContext : IdentityDbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}



		public DbSet<Item> Items { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderdItem> OrderdItems { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }

		public DbSet<ApplicationUser> AppUsers { get; set; }
	}

}
