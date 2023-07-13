using Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Common
{
    public class ValidateCommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IList<IValidator<TRequest>> _validators;

        public ValidateCommandBehavior(IList<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Select(s => s.ErrorMessage).ToList()
                .ToList();

            if (errors.Any())
            {
                throw new MultiplyMessageException(errors);
            }

            return next();
        }
    }
}
