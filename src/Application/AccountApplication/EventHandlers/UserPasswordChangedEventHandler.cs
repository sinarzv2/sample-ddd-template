using Domain.Aggregates.Identity.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.AccountApplication.EventHandlers
{
	public sealed class UserPasswordChangedEventHandler : INotificationHandler<UserPasswordChangedEvent>
    {
        private readonly ILogger<UserPasswordChangedEventHandler> _logger;
        public UserPasswordChangedEventHandler(ILogger<UserPasswordChangedEventHandler> logger)
        {
            _logger = logger;
		}


        public Task Handle(UserPasswordChangedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(UserPasswordChangedEventHandler)} Done!");

            return Task.CompletedTask;
        }
    }
}
