using Common.Constant;
using Domain.Aggregates.Identity;
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
            //var adminUser =  UserManager.FindByNameAsync("Admin").Result;
            //if (adminUser == null)
            //{
            //    var user = new User()
            //    {
            //        FullName = "Admin",
            //        UserName = "Admin",
            //        Email = "admin@admin.com"
            //    };
            //    var result = UserManager.CreateAsync(user, "123456").Result;
            //    if (result.Succeeded)
            //    {
            //        var resultUserRole = UserManager.AddToRoleAsync(user, ConstantRoles.Admin).Result;
            //        if(!resultUserRole.Succeeded)
            //            throw new Exception(string.Join(" | ", resultUserRole.Errors.Select(d => d.Description)));
            //    }
            //    else
            //    {
            //        throw new Exception(string.Join(" | ", result.Errors.Select(d => d.Description)));
            //    }
            //}
        }
    }
}
