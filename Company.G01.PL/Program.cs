using Company.G01.BLL;
using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.Mapping.Employees;
using Company.G01.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.G01.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped<AppDbContext>(); // allow DI for AppDbContext

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>(); // Allow DI For DepartmentRepository
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Allow DI For EmployeeRepository

           
            //// Life Time
            //builder.Services.AddScoped();    // Per Request, UnReachable Object
            //builder.Services.AddSingleton(); // Per App
            //builder.Services.AddTransient(); // Per Operations
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(EmployeeProfile));


			builder.Services.AddScoped<IScopedService, ScopedService>();
            builder.Services.AddSingleton<ISingeltonService, SingeltonService>();
            builder.Services.AddTransient<ITransientService, TransientService>();
            //builder.Services.AddScoped<UserManager<ApplicationUser>>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });

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

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
