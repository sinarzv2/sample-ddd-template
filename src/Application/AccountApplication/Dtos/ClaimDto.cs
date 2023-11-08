namespace Application.AccountApplication.Dtos;

public class ClaimDto
{
    public Guid Id { get; set; }
    public required string ClaimType { get; set; }
    public required string ClaimValue { get; set; }
}