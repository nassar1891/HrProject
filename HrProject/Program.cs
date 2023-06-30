using HrProject.Data.DataInitilaizer;
using HrProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HrProject
{
	public class Program
	{
		public static void Main(string[] args)
		{
			

			var builder = WebApplication.CreateBuilder(args);
			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<HrContext>(
				option => option.UseSqlServer(builder.Configuration.GetConnectionString("hrConnection")));

			builder.Services.AddIdentity<HrUser, IdentityRole>(
				option =>
				{
					option.Password.RequireNonAlphanumeric = false;
					option.Password.RequiredLength = 5;
				}).AddEntityFrameworkStores<HrContext>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			DataInitilizer.Configure(app);

			app.Run();
		}
	}
}