using Common.DependencyLifeTime;
using Common.Models;
using Domain.Aggregates.Identity;
using System.Security.Claims;
using Application.AccountApplication.Dtos;

namespace Application.Common.JwtServices
{
    public interface IJwtService : IScopedService
    {
        Task<TokenDto> GenerateAsync(User user);

        FluentResult<ClaimsPrincipal> GetPrincipalFromExpiredToken(string accessToken);
    }
}
