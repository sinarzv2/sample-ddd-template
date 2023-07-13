using Common.Models;
using MediatR;

namespace Domain.SeedWork
{
    public interface ICommand : IRequest<FluentResult>
    {
    }

    public interface ICommand<TResponse> : IRequest<FluentResult<TResponse>> where TResponse : class
    {
    }
}
