using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services
{
    public class Initialization
    {
        public Initialization(IServiceCollection services)
        {
            this.ConfigureServices(services);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var dataInit = new Data.Initialization(services);
        }
    }
}
