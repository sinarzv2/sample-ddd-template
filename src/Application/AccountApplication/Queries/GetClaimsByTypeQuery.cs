using Application.AccountApplication.Dtos;
using Domain.SeedWork;

namespace Application.AccountApplication.Queries
{
    public class GetClaimsByTypeQuery : IQuery<List<ClaimDto>>
    {
        public Guid UserId { get; init; }
        public required string Type { get; init; }
    }
}
