namespace Infrastructure.Models.Account
{
    public class ClaimModel
    {
        public Guid Id { get; init; }
        public required string? Type { get; init; }
        public required string? Value { get; init; }
    }
}
