using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Shawna_Staff.Models;
using Shawna_Staff.Repos;

namespace Shawna_Staff
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
            services.AddTransient<IForums, ForumsRepository>();
            services.AddDbContext<ForumContext>(options => 
            options.UseSqlServer(Configuration["ConnectionStrings:ConnectionString"]));

            services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ForumContext>()
            .AddDefaultTokenProviders();

            services.AddResponseCaching((options) =>
            {              
                options.MaximumBodySize = 1024;
                options.UseCaseSensitivePaths = true;
            });
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
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    // Disable caching for all static files.
                    context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                    context.Context.Response.Headers["Pragma"] = "no-cache";
                    context.Context.Response.Headers["Expires"] = "-1";
                }
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });

            app.UseResponseCaching();
            app.Use(async (context, next) =>
            {
                if (context.Request.Method.Equals(System.Net.Http.HttpMethod.Get))
                {
                    context.Response.GetTypedHeaders().CacheControl =
                    new CacheControlHeaderValue()
                    {
                        Private = true,
                        MaxAge = TimeSpan.FromSeconds(10),
                        NoCache = true,
                        NoStore = true,
                        MustRevalidate = true
                        
                    };
                    context.Response.Headers[HeaderNames.Vary] =
                        new string[] { "Accept-Encoding" };
                }
                await next();
            });
            HttpResponseMessage response = new HttpResponseMessage();
            response.Headers.Pragma.ParseAdd("no-cache");

            CookieOptions cookie = new CookieOptions
            {
                Domain = "https://landscapephotography.azurewebsites.net/",    // replace this with your live site URL
                Path = "/"                                                      // this allows the cookie access to the root
            };

            DBInitializer.CreateAdminUser(app.ApplicationServices).Wait();
            DBInitializer.CreateMemberUser(app.ApplicationServices).Wait();
        }
    }
}
