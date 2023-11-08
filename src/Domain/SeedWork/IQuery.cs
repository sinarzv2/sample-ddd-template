using Common.Models;
using MediatR;

namespace Domain.SeedWork;

public interface IQuery : IRequest<FluentResult>
{
}

public interface IQuery<TResponse> : IRequest<FluentResult<TResponse>>
{
}