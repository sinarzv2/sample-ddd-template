using Azure.Core;
using Common.Models;
using Common.Utilities;
using Domain.Aggregates.Identity;
using Domain.Aggregates.Identity.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountApplication.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> ExistUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }

      
    }
}
