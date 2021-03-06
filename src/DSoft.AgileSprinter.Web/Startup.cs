﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DSoft.AgileSprinter.Web.Services;
using DSoft.AgileSprinter.Data;
using DSoft.AgileSprinter.Data.Models;
using DSoft.AgileSprinter.Web.Models;

namespace DSoft.AgileSprinter.Web
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
            //Add database context
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //The DSoftAgileSprinter connection string is stored in the user secrets. Use "SetUserSecretsForAgileSprinter.bat" file to solve.
            services.AddDbContext<DSoft_AgileSprinterContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DSoftAgileSprinter")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //Change default password requirements. Might want to modify later
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 6; //IF YOU CHANGE THIS, YOU MUST CHANGE IT ON THE REGISTRATION PAGES ATTRIBUTES
            });

            //Configuration values are supplied on each individual machine. 
            //To set yours up, get the "SetUserSecretsForAgileSprinter.bat" file from another developer and follow the instructions
            services.AddAuthentication()
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration.GetSection("Authentication")["GoogleClientID"];
                    googleOptions.ClientSecret = Configuration.GetSection("Authentication")["GoogleSecretID"];
                })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration.GetSection("Authentication")["FacebookAppID"];
                    facebookOptions.AppSecret = Configuration.GetSection("Authentication")["FacebookSecretID"];
                })
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = Configuration.GetSection("Authentication")["MicrosoftAppID"];
                    microsoftOptions.ClientSecret = Configuration.GetSection("Authentication")["MicrosoftSecretID"];
                });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
