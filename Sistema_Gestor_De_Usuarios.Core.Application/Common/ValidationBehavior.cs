
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

public class ValidationBehavior<T> : IEndpointFilter
{
    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

        if (validator is null) return await next(context);

        var inputToValidate = context.Arguments.OfType<T>().FirstOrDefault();

        if (inputToValidate is null)
            return TypedResults.Problem("Entity to validated could not be found.");

        var validationResult = await validator.ValidateAsync(inputToValidate);

        if (!validationResult.IsValid)
        {
            var firstError = validationResult.Errors.FirstOrDefault()?.ErrorMessage;
            var errorResponse = new { mensaje = firstError };
            return TypedResults.BadRequest(errorResponse);
        }

        return await next(context);
    }
}
