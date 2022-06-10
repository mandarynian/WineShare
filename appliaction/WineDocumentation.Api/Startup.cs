using System;
using System.Text;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;
using WineDocumentation.Core.Repositoies;
using WineDocumentation.Infrastructure.DTO;
using WineDocumentation.Infrastructure.Repository;
using WineDocumentation.Infrastructure.Service;
using WineDocumentation.Infrastructure.Mapper;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using WineDocumentation.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using WineDocumentation.Api.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;

namespace WineDocumentation.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(
                MyAllowSpecificOrigins, 
                p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            ));




            services.AddScoped<IUserRepository, InMemoryUserRepository>();
            services.AddScoped<IWineRepository, InMemoryWineRepository>();
            // services.AddScoped<IUserRepository, UserRepository>();
            //services.AddEntityFrameworkNpgsql().AddDbContext<WineDocumentationContex>(opt => 
            //opt
            //.UseNpgsql(Configuration.GetSection("ConnectionString:MyWebConnectionString").Value)
            //);

            // services.AddDistributedMemoryCache();

            // services.AddSession(options =>
            // {
            //     options.Cookie.Name = ".AdventureWorks.Session";
            //     options.IdleTimeout = TimeSpan.FromMinutes(30);
            //     options.Cookie.IsEssential = true;
            // });
        
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/";
                    options.Cookie.Name = "CookieApp";
                });

            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IWineRepository, WineRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWineService, WineService>();
            services.AddSingleton(AutoMapperConfiguration.Initialize());
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddControllers();
            services.AddMvc();
            
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
            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
