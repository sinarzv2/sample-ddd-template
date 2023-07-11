using System;
using System.Linq;
using Common.Constant;
using Domain.Aggregates.Identity;
using Domain.SharedKernel.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace Application.GeneralServices.DataInitializer
{
    public class UserDataInitializer : IDataInitializer
    {
        public UserDataInitializer(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        protected  UserManager<User> UserManager { get; }
        public void InitializeData()
        {
            var adminUser =  UserManager.FindByNameAsync("Admin123").Result;
            if (adminUser == null)
            {
                var pass = "12345678";
                var userResult = User.Create("Admin123", pass, "admin@admin.com", "09354831413", Gender.Male.Value, "Admin",
                    "Admin", new DateTime(1991, 5, 29));
                if (!userResult.IsSuccess)
                    throw new Exception(string.Join(" | ", userResult.Errors.Select(d => d)));

                var user = userResult.Data;
                var result = UserManager.CreateAsync(user, pass).Result;
                if (result.Succeeded)
                {
                    var resultUserRole = UserManager.AddToRoleAsync(user, ConstantRoles.Admin).Result;
                    if(!resultUserRole.Succeeded)
                        throw new Exception(string.Join(" | ", resultUserRole.Errors.Select(d => d.Description)));
                }
                else
                {
                    throw new Exception(string.Join(" | ", result.Errors.Select(d => d.Description)));
                }
            }
        }
    }
}
