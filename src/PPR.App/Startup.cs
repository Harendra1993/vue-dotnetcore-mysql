using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PPR.App.DTOs;
using PPR.App.Providers;
using PPR.Business.Interfaces;
using PPR.Business.Repositories;
using PPR.Common.Entities;

namespace PPR.App {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration.GetValue<string> ("JwtIssuer"),
                    ValidAudience = Configuration.GetValue<string> ("JwtAudience"),
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (Configuration.GetValue<string> ("JwtSecretKey")))
                    };
                });

            // Add framework services.
            services.AddMvc ()
                .SetCompatibilityVersion (CompatibilityVersion.Version_2_1);

            // Simple example with dependency injection for a data provider.
            services.AddSingleton<IWeatherProvider, WeatherProviderFake> ();
            services.AddSingleton<IAccountRepository, AccountRepository> ();
            services.AddSingleton<IDashboardRepository, DashboardRepository> ();

            Mapper.Initialize (cfg => {
                cfg.CreateMap<User, UserDTO> ();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();

                // Webpack initialization with hot-reload.
                app.UseWebpackDevMiddleware (new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true,
                });
            } else {
                app.UseExceptionHandler ("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            //  app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            app.UseAuthentication ();

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute (
                    name: "spa-fallback",
                    defaults : new { controller = "Home", action = "Index" });
            });

        }
    }
}