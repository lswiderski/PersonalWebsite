using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsite.Data
{
    public class Initialization
    {
        public Initialization(IServiceCollection services)
        {
            this.ConfigureServices(services);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = "";
            try
            {
                string connectionStringPath = "ConnectionString.txt";

                using (StreamReader sr = File.OpenText(connectionStringPath))
                {
                    connection = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            services.AddDbContext<DataContext>(options => options.UseSqlServer(@connection, b =>b.MigrationsAssembly("PersonalWebsite.Data")));

        }
    }
}
