using Domain.Aggregates.Identity.ValueObjects;

namespace Domain.Test.Aggregates.Identity.ValueObjects;

public class RefreshTokenTest
{
    [Fact]
    public void Create_CreateRefreshToken_ResultIsSuccess()
    {
        var result = RefreshToken.Create();

        Assert.True(result.IsSuccess);
            
        Assert.NotEmpty(result.Data.Token);
        Assert.Equal(result.Data.ExpiryTime.Date, DateTime.Now.AddDays(RefreshToken.ExpirationRefreshTimeDays).Date);
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_CleareToken_ResultIsSuccess()
    {
        var result = RefreshToken.Create();

        Assert.True(result.IsSuccess);

        var refreshToken = result.Data;

        refreshToken.ClearToken();

        Assert.Empty(result.Data.Token);
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }
}