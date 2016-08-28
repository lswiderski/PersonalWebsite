using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.Services.Models;
using PersonalWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalWebsite.IdentityModel;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace PersonalWebsite.Services
{
    public class Initialization
    {

        private Data.Initialization dataInit;
        public Initialization(IServiceCollection services)
        {
            this.ConfigureServices(services);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            dataInit = new Data.Initialization(services);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddTransient<ISettingModel,SettingModel>();

        }

        public void Configure(IServiceProvider serviceProvider)
        {
            dataInit.Configure(serviceProvider);
        }
    }
}
