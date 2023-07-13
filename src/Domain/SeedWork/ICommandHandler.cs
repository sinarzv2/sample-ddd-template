using Common.Models;
using MediatR;

namespace Domain.SeedWork
{
    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest, FluentResult> where TRequest : IRequest<FluentResult>
    {
    }

    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, FluentResult<TResponse>> where TResponse : class where TRequest : IRequest<FluentResult<TResponse>>
    {
    }
}
