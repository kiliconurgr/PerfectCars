using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCPerfectCars.Service;
using MVCPerfectCarsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPerfectCars
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
            services.AddControllersWithViews();
            services.AddDbContext<MVCPerfectCarsDbContext>(optionsAction=> 
            {
                optionsAction.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });
            services.AddIdentity<User, Role>(optionAction => { 


            }).AddEntityFrameworkStores<MVCPerfectCarsDbContext>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddSingleton<UtilsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MVCPerfectCarsDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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
            app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<MVCPerfectCarsDbContext>().Database.Migrate();
            app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IAccountService>().InitAsync().Wait();
            app.UseEndpoints(endpoints =>
            {
                endpoints
                   .MapControllerRoute(
                       name: "vehicle",
                       pattern: "{name}-v-{id}.html",
                       defaults: new { controller = "Home", action = "VehicleDetail" }
                       );
                endpoints
               .MapControllerRoute(
                   name: "brand",
                   pattern: "{name}-b-{id}.html",
                   defaults: new { controller = "Home", action = "Brands" }
                   );
                endpoints
                .MapControllerRoute(
                   name: "modul",
                   pattern: "{name}-m-{id}.html",
                   defaults: new { controller = "Home", action = "Modul" }
                   );
                endpoints.MapControllerRoute(
                     name: "areas",
                     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            context.Database.Migrate();


        }
    }
}
