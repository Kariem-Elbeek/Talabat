﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Kariem",
                    UserName = "Abdelkariem",
                    Email = "abdelkariem@email.com",
                    PhoneNumber = "0111222333"
                };
                await userManager.CreateAsync(user, "Pa$$W0rd");
            }
        }
    }
}
