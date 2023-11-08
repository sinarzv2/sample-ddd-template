using Application.AccountApplication.Dtos;
using Common.Constant;
using Common.Models;
using Common.Resources.Messages;
using Domain.Aggregates.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Common.JwtServices;

public class JwtService : IJwtService
{
    private readonly SignInManager<User> _signInManager;
    private readonly SiteSettings _siteSettings;
    public JwtService(IOptionsSnapshot<SiteSettings> siteSetting, SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
        _siteSettings = siteSetting.Value;
    }
    public async Task<TokenDto> GenerateAsync(User user)
    {
        var secretKey = _siteSettings.JwtSettings.SecretKey;
        var encryptionkey = _siteSettings.JwtSettings.EncryptKey;
        var claims = await GetClaimsAsync(user);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature);
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(encryptionkey)), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
        var descriptor = new SecurityTokenDescriptor()
        {
            Audience = _siteSettings.JwtSettings.Audience,
            Issuer = _siteSettings.JwtSettings.Issuer,
            IssuedAt = DateTime.Now,
            Expires = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.ExpirationMinutes),
            NotBefore = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.NotBeforeMinutes),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
        var token = new TokenDto(securityToken);
        return token;
    }

    public FluentResult<ClaimsPrincipal> GetPrincipalFromExpiredToken(string accessToken)
    {
        var result = new FluentResult<ClaimsPrincipal>();
        var secretKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.SecretKey);

        var encryptionkey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.EncryptKey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = _siteSettings.JwtSettings.Audience,
            ValidIssuer = _siteSettings.JwtSettings.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateLifetime = false,
            TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
        };
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.Aes128KW,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                result.AddError(Errors.InvalidToken);
                return result;
            }
            result.SetData(principal);
            return result;

        }
        catch (Exception)
        {
            result.AddError(Errors.InvalidToken);
            return result;
        }
           
    }



    private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
    {
        var result = await _signInManager.ClaimsFactory.CreateAsync(user);
        return _siteSettings.UseTokenClaim ? result.Claims :
            result.Claims.Where(d => d.Type != ConstantClaim.Permission && d.Type != ClaimTypes.Role);
    }
}