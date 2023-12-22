using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Behaviors;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Validar si existe alguna validacion que se ha saltado
        if(!_validators.Any())
        {
            // Si no existe se salte el sig paso
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        // Obtener errores cuando el cliente envia mal la data
        var validationErrors = _validators.Select(validator => validator.Validate(context))
        .Where(validationResult => validationResult.Errors.Any())
        .SelectMany(validationResult => validationResult.Errors)
        .Select(validationFailure => new ValidationError(
            validationFailure.PropertyName,
            validationFailure.ErrorMessage
        )).ToList();

        if(validationErrors.Any()){
            throw new Exceptions.ValidationException(validationErrors);
        }

        return await next();
    }
}