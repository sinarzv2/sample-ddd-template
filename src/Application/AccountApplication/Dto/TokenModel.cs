using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

namespace Application.AccountApplication.Dto
{
    public class TokenModel
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        public TokenModel(JwtSecurityToken securityToken)
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            TokenType = "Bearer";
            ExpiresIn = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        }
    }
}
