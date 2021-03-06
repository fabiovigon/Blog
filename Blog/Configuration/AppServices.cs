using Blog.Authorization;
using Blog.BusinessManager;
using Blog.BusinessManager.interfaces;
using Blog.Data;
using Blog.Data.Models;
using Blog.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Blog.Service.Interfaces;
using post.Service;
using post.BusinessManager;

namespace Blog.Configuration
{
    public static class AppServices
    {
        public static void AddDefaultServices(this IServiceCollection servicesCollection, IConfiguration configuration)
        {
            servicesCollection.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(
                  configuration.GetConnectionString("DefaultConnection")));
            servicesCollection.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            servicesCollection.AddControllersWithViews().AddRazorRuntimeCompilation();
            servicesCollection.AddRazorPages();
            servicesCollection.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }

        public static void AddCustomServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPostBusinessManager, PostBusinessManager>();
            serviceCollection.AddScoped<IPostService, PostService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IHomeBusinessManager, HomeBusinessManager>();
            serviceCollection.AddScoped<IAdminBusinessManager, AdminBusinessManager>();
        }

        public static void AddCustomAuthorization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAuthorizationHandler, PostAuthorizationHandler>();
        }
    }
}
