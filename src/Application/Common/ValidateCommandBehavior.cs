using Application.Common.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.OutputCaching;

namespace Application.Common
{
    public class ValidateCommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IList<IValidator<TRequest>> _validators;

        public ValidateCommandBehavior(IList<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var list = new List<string>();
            foreach (var validator in _validators)
            {
                var validation = await validator.ValidateAsync(request, cancellationToken);
                var errors = validation.Errors.Select(s => s.ErrorMessage).ToList();
                list.AddRange(errors);
            }

            //var errors2 = _validators
            //    .Select(v => v.Validate(request))
            //    .SelectMany(result => result.Errors)
            //    .Select(s => s.ErrorMessage)
            //    .ToList();

            if (list.Any())
                throw new MultiplyMessageException(list);

            return await next();
        }
    }
}
