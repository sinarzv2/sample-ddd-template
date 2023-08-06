using Application.AccountApplication.ViewModels;
using Common.DependencyLifeTime;
using Common.Models;
using Domain.Aggregates.Identity;
using System.Security.Claims;

namespace Application.Common.JwtServices
{
    public interface IJwtService : IScopedService
    {
        Task<TokenModel> GenerateAsync(User user);

        FluentResult<ClaimsPrincipal> GetPrincipalFromExpiredToken(string accessToken);
    }
}
