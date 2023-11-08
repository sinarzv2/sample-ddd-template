namespace Common.Models;

public class SiteSettings
{
    public JwtSettings JwtSettings { get; set; }
    public IdentitySettings IdentitySettings { get; set; }
    public RedisSettings RedisSettings { get; set; }
    public string LoginUrl { get; set; }
    public bool UseTokenClaim { get; set; }

}

public class RedisSettings
{
    public string Connection { get; set; }
    public string InstanceName { get; set; }
    public bool IsEnabled { get; set; }
    public string Password { get; set; }

}
public class IdentitySettings
{
    public bool PasswordRequireDigit { get; set; }
    public int PasswordRequiredLength { get; set; }
    public bool PasswordRequireNonAlphanumic { get; set; }
    public bool PasswordRequireUppercase { get; set; }
    public bool PasswordRequireLowercase { get; set; }
    public bool RequireUniqueEmail { get; set; }
}
public class JwtSettings
{
    public string SecretKey { get; set; }
    public string EncryptKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int NotBeforeMinutes { get; set; }
    public int ExpirationMinutes { get; set; }
}