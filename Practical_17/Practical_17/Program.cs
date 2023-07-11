using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Practical_17.Data;
using Practical_17.Interfaces;
using Practical_17.Middleware;
using Practical_17.Repositories;

namespace Practical_17
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			//builder.Services.AddControllersWithViews();

			builder.Services.AddMvc();

			builder.Services.AddDbContext<DatabaseContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
			});


			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
			{
				options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
				options.AccessDeniedPath = "/Account/AccessDenied";
				options.LoginPath = "/Account/Login";
				options.LogoutPath = "/Account/Logout";
			});

			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(10);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			builder.Services.AddScoped<IStudentRepository, StudentRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();

			var app = builder.Build();



			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseErrorHandaling();
			app.UseRouting();
			app.UseSession();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}"
			);

			app.Run();
		}
	}
}