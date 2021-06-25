using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDS.Data.EF;
using BDS.Data.Entities;
using BDS.Services.Area;
using BDS.Services.News;
using BDS.Services.Project;
using BDS.Services.RealEstate;
using BDS.Services.Recruitment;
using BDS.Services.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BDS.WebApp
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

            services.AddDbContext<BdsDbContext>(options => 
                options.UseNpgsql(@"Server=localhost;Port=5432;Database=BDS;User Id=postgres;Password=admin")
                );
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<BdsDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/User/Login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });


            // DI
            services.AddTransient<IProjectService,ProjectService>();
            services.AddTransient<IAreaService,AreaService>();
            services.AddTransient<IRealEstateService,RealEstateService>();
            services.AddTransient<INewsService,NewsService>();
            services.AddTransient<IRecruitmentService,RecruitmentService>();
            services.AddTransient<UserManager<User>,UserManager<User>>();
            services.AddTransient<SignInManager<User>,SignInManager<User>>();
            services.AddTransient<IUserService,UserService>();
            


            
            
            services.AddControllersWithViews();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}