using AutoMapper;
using Business.providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "Server=DESKTOP-CJ79M32;Database=ToDoList;User ID=vlad;Password=varkobl09var";
            services.AddTransient<ITaskProvider, TaskRepository>(provider => new TaskRepository(connectionString));
            services.AddTransient<ICategoryProvider, CategoryRepository>(provider => new CategoryRepository(connectionString));
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Task}/{action=Index}/{id?}");
            });
        }
    }
}
