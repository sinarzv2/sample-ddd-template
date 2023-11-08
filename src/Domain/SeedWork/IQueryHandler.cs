using Common.Models;
using MediatR;

namespace Domain.SeedWork;

public interface IQueryHandler<in TRequest> : IRequestHandler<TRequest, FluentResult> where TRequest : IRequest<FluentResult>
{
}

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, FluentResult<TResponse>>  where TRequest : IRequest<FluentResult<TResponse>>
{
}