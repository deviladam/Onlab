﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Szertar.Dal.Entities;
using Szertar.Dal;
using Szertar.Dal.Managers;
using Szertar.Dal.Managers.Interfaces;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Szertar
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseLazyLoadingProxies().UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IItemManager, ItemManager>();
			services.AddScoped<ICartManager, CartManager>();
			services.AddScoped<IOrderManager, OrderManager>();
			services.AddScoped<IUserManager, UserManager>();

			services.AddDefaultIdentity<ApplicationUser>()
				.AddRoles<IdentityRole>()
				.AddDefaultUI(UIFramework.Bootstrap4)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddAuthentication().AddOpenIdConnect("Google","Google", o =>
			 {
				 o.ClientId = Configuration["Authentication:Google:ClientId"];
				 o.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
				 o.Authority = "https://accounts.google.com";
				 o.ResponseType = OpenIdConnectResponseType.Code;
				 o.CallbackPath = "/signin-google"; // Or register the default "/sigin-oidc"
				 o.Scope.Add("email");
			 });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
			
		}
	}
}
