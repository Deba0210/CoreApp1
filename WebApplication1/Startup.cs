using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
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
            services.AddDbContext<QuickKartDBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("CoreAppDBConnection")));
            services.AddScoped<IRepository, ModelRepository>();
            services.AddScoped<IRepositoryNew, ModelRepositoryNew>();
            services.AddIdentity<IdentityUser, IdentityRole>(
                option=> {
                    option.Password.RequiredLength = 8;
                    option.SignIn.RequireConfirmedEmail = true;
                }).AddEntityFrameworkStores<QuickKartDBContext>();
            //services.

            services.AddCloudscribePagination();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/useError");
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Home/useError");
                //app.UseExceptionHandler("/Home/useError");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=IdentityHome}/{action=LogIn}/{id?}");
            });
        }
    }
}
