using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.Services.Models;
using PersonalWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using PersonalWebsite.IdentityModel;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using PersonalWebsite.Common;


namespace PersonalWebsite.Services
{
    public class Initialization
    {

        private Data.Initialization dataInit;
        public Initialization(IServiceCollection services, IConfigurationRoot configuration)
        {
            this.ConfigureServices(services, configuration);

        }
        public void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            dataInit = new Data.Initialization(services, configuration);

            services.AddMemoryCache();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
           
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<IXmlFeedService, XmlFeedService>();

            ConfigureDataServices(services);
            
        }

        private void ConfigureDataServices(IServiceCollection services)
        {
            services.AddTransient<ISettingModel, SettingModel>();
            services.AddTransient<IPostModel, PostModel>();
            services.AddTransient<ITagModel, TagModel>();
            services.AddTransient<ICategoryModel, CategoryModel>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IImageService, ImageService>();
        }

        public void Configure(IServiceProvider serviceProvider)
        {
            dataInit.Configure(serviceProvider);
        }

    }
}
