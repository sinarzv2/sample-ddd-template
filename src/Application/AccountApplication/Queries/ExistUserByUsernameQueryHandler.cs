using Common.Models;
using Domain.Aggregates.Identity;
using Domain.SeedWork;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountApplication.Queries
{
    public class ExistUserByUsernameQueryHandler : IQueryHandler<ExistUserByUsernameQuery,bool>
    {
        private readonly UserManager<User> _userManager;

        public ExistUserByUsernameQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<FluentResult<bool>> Handle(ExistUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var result = new FluentResult<bool>();
            var user = await _userManager.FindByNameAsync(request.UserName);
            result.SetData(user != null);
            return result;
        }
    }
}
