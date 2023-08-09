namespace Application.AccountApplication.Dtos
{
    public class ClaimDto
    {
        public Guid Id { get; set; }
        public required string Type { get; set; }
        public required string Value { get; set; }
    }
}
