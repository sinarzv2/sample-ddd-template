using Domain.Aggregates.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountApplication.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ExistUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }


    }
}
