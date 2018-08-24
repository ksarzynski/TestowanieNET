using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cinema.Data;
using Cinema.Models;
using Cinema.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Cinema
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
      
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<ApplicationDbInitializer>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IFilmRepository, FilmRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, ApplicationDbInitializer dbInitializer)
        {
            

            app.UseExceptionHandler(e => this.CaptureException(e));

            app.UseStaticFiles();

            app.UseAuthentication();

            dbInitializer.Seed();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
       }
             
        private IApplicationBuilder CaptureException(IApplicationBuilder options)
        {
            options.Run(
                  async context =>
                  {
                      context.Response.ContentType = "text/html";
                      var ex = context.Features.Get<IExceptionHandlerFeature>();
                      if (ex != null)
                      {
                          var err = $"<h2><b>Error:</b><br> {ex.Error.Message}</h2>{ex.Error.StackTrace }";
                          await context.Response.WriteAsync(err);
                      }
                  });
            return options;
        }

    }
}

