﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
                ValidationResult[] validationsResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                List<ValidationFailure> failures = validationsResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count > 0)
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
