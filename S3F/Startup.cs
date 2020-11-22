using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using SPS.Models.Context;
using PersianTranslation.Identity;

namespace SPS
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
            //اجرا مدل و کنترل ها
            services.AddControllersWithViews().AddRazorRuntimeCompilation();// افزودن قابلیت رندر مجدد ریزور در زمان اجرا

            //اضافه کردن سرویس بانک اطلاعاتی
            services.AddDbContextPool<AppDbContext>(op =>
            {
                op.UseSqlServer(Configuration.GetConnectionString("sqlServerConn"));
            });

            //افزودن سرویس احراز هویت
            services.AddIdentity<IdentityUser, IdentityRole>(option =>
            {
                option.Password.RequiredUniqueChars = 0;
                option.User.RequireUniqueEmail = true;
                option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);


            }).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders().AddErrorDescriber<PersianIdentityErrorDescriber>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseAuthentication();//احراز هویت
            app.UseAuthorization();//مجوزها

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
