using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterProject.Models;

namespace WaterProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940


        public IConfiguration Configuration { get; set; }
        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<WaterProjectContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:WaterDBConnection"]);
            });
            services.AddScoped<IWaterProjectRepository, EFWaterProjectRepository>();
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDonationRepository, EFDonationRepository>();

            services.AddServerSideBlazor();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }


            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("typepage", "{ProjectType}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" });


                //the order of these matter it does the top to bottom if the top works then it skips the rest. 
                //this makes it so that there is not slug just the page number. 
                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "{PageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

                //this is so when we click on the link on the left it goes to page 1 first. 
                endpoints.MapControllerRoute("type",
                    "{ProjectType}",
                    new { Controller = "Home", Action = "Index", pageNum = 1 });

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");


                
            });
        }
    }
}
