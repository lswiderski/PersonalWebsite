using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Data
{
    public class Initialization
    {
        public Initialization()
        {

        }
        public void ConfigureServices(IServiceCollection services)
        {

            var connection = @"Server=DESKTOP-14EBE41\SQLEXPRESS;Database=PersonalWebsite;Trusted_Connection=True;";
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection, b =>b.MigrationsAssembly("PersonalWebsite.Data")));

        }
    }
}
