using FluentValidation;
using MediatR;

namespace OCASAPI.Application.Behaviours
{
    /// <summary>
    /// Validation behaviour class, inherits from <see cref="MediatR.IPipelineBehavior{TRequest, TResponse}"/>
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Handle method, part of the mediatR request pipeline is used to validate mediatR requests before further data processing
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="next"></param>
        /// <returns>
        /// Collection of <see cref="FluentValidation.Results.ValidationFailure"/> for a given request
        /// </returns>
        /// <pararef name="request" />
        /// </exception>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new FluentValidation.ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new Exceptions.ValidationException(failures);
            }
            return await next();
        }
    }
}