using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.Data.Entities;
using PersonalWebsite.IdentityModel;
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
            string connection = @"Server=DESKTOP-14EBE41\SQLEXPRESS;Database=PersonalWebsite;Trusted_Connection=True;";

            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection, b =>b.MigrationsAssembly("PersonalWebsite.Data")));
           
        }
        public void Configure(IServiceProvider serviceProvider)
        {
            this.SetInitRoles(serviceProvider);
        }
        private void SetInitRoles(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<DataContext>();

            string[] roles = new string[] { "Administrator", "User", "Editor", };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }


            var user = new ApplicationUser
            {
                Email = "lukasz@swiderski.xyz",
                NormalizedEmail = "LUKASZ@SWIDERSKI.XYZ",
                UserName = "lukasz@swiderski.xyz",
                NormalizedUserName = "LUKASZ@SWIDERSKI.XYZ",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "secret");
                user.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(context);
                var result = userStore.CreateAsync(user);
                var resultRole = userStore.AddToRoleAsync(user, "Administrator");

            }

            //AssignRoles(serviceProvider, user.Email, roles);

            context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}
