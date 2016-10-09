using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.Common.Enums;
using PersonalWebsite.Data.Entities;
using PersonalWebsite.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.Data.SeedData
{
    public static class DataContextSeedData
    {
        public static void SeedData(this IServiceScopeFactory scopeFactory)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();

                CreateRoles(context);
                CreateSettings(context);

                context.SaveChanges();
            }
        }

        private static void CreateRoles(DataContext context)
        {
            string[] roles = new string[] { "Administrator", "User", "Editor", };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    var x = roleStore.CreateAsync(new IdentityRole(role)).Result;
                }
            }

            var user = new ApplicationUser
            {
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "admin");
                user.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(context);
                var result = userStore.CreateAsync(user).Result;
                var resultRole = userStore.AddToRoleAsync(user, "Administrator");
            }
            // var userResult = userManager.FindByEmailAsync(user.Email).Result;
            // var rolesResult = userManager.AddToRolesAsync(userResult, roles).Result;
        }

        private static void CreateSettings(DataContext context)
        {
            var settings = new List<Setting>()
            {
                new Setting {Code = "Blog.PageSize", Name = "Blog.PageSize", Type = SettingDataType.INT, Value = "5"},
                new Setting {Code = "Socials.Facebook.Profile.Link", Name = "Facebook Profile Link", Type = SettingDataType.STRING, Value = ""},
                new Setting {Code = "Socials.Twitter.Profile.Link", Name = "Twitter Profile Link", Type = SettingDataType.STRING, Value = ""},
                new Setting {Code = "Socials.Github.Profile.Link", Name = "Github Profile Link", Type = SettingDataType.STRING, Value = ""},
                new Setting {Code = "Socials.Instagram.Profile.Link", Name = "Instagram Profile Link", Type = SettingDataType.STRING, Value = ""},
                new Setting {Code = "Socials.LinkedIn.Profile.Link", Name = "LinkedIn Profile Link", Type = SettingDataType.STRING, Value = ""},
                new Setting {Code = "Socials.Mail", Name = "Mail", Type = SettingDataType.STRING, Value = ""},
                new Setting {Code = "Instagram.AccessToken", Name = "Instagram AccessToken", Type = SettingDataType.STRING, Value = ""},
                new Setting {Code = "Instagram.UserID", Name = "Instagram UserID", Type = SettingDataType.STRING, Value = ""},
                new Setting {Code = "Twitter.Widget.ID", Name = "Twitter Widget.ID", Type = SettingDataType.STRING, Value = ""},
                new Setting {Code = "Twitter.UserName", Name = "Twitter UserName", Type = SettingDataType.STRING, Value = ""},
                new Setting {Code = "Website.Name", Name = "Website Name", Type = SettingDataType.STRING, Value = "Website"},
                new Setting {Code = "Website.Description", Name = "Website Description", Type = SettingDataType.STRING, Value = "Personal Website"}
            };

            foreach (var setting in settings)
            {
                if (!context.Settings.Any(r => r.Code == setting.Code))
                {
                    context.Add(setting);
                }
            }
        }
    }
}