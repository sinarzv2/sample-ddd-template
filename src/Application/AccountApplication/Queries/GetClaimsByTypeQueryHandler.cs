using Common.Constant;
using Common.Models;
using Domain.Aggregates.Identity;
using Domain.SeedWork;
using MapsterMapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Application.AccountApplication.Dtos;
using Common.Resources.Messages;
using Application.UnitOfWork;

namespace Application.AccountApplication.Queries
{
    public class GetClaimsByTypeQueryHandler : IQueryHandler<GetClaimsByTypeQuery, List<ClaimDto>>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClaimsByTypeQueryHandler(IDistributedCache distributedCache, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _distributedCache = distributedCache;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FluentResult<List<ClaimDto>>> Handle(GetClaimsByTypeQuery request, CancellationToken cancellationToken)
        {
            var result = new FluentResult<List<ClaimDto>>();
            var claims = await _distributedCache.GetStringAsync(CacheKeys.ClaimsKey(request.Type, request.UserId), token: cancellationToken);
            if (claims != null)
            {
                var list = JsonSerializer.Deserialize<List<ClaimDto>>(claims);
                if (list == null)
                {
                    result.AddError(Errors.Deserilize);
                    return result;
                }
                result.SetData(list);
                return result;
            }
            else
            {
                var userClaim = await _unitOfWork.UserClaimRepository.GetClaimsByType(request.UserId, request.Type);
                var list = _mapper.Map<List<ClaimDto>>(userClaim);
                await _distributedCache.SetStringAsync(CacheKeys.ClaimsKey(request.Type, request.UserId),
                    JsonSerializer.Serialize(list), token: cancellationToken);
                result.SetData(list);
                return result;
            }
          
        }
    }
}
