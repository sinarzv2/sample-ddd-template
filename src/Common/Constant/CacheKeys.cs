namespace Common.Constant;

public static class CacheKeys
{
    public static string ClaimsKey(string type, Guid userId) => $"ClaimsKey_{type}_{userId}";
}

