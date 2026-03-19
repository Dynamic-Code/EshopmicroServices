using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviours
{
    // Created a generic class for validation form MedaitR IPipelineBehavior
    // this act as a middleware, thats why we have next parameters
    public class ValidationBehavior<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators) //Inject Ivalidator using primary constructor
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse> // Added a filter. means we are appying all this validations behaviour in CommandHandler CRUD ops
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            //Check Validation error in incoming requset
            var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

            var failures = validationResults.Where(x => x.Errors.Any()).SelectMany(x => x.Errors).ToList();

            if (failures.Any())
                throw new ValidationException(failures);

            return await next();
        }
    }
}
