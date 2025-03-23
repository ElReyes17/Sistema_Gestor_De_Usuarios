using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.User;
using Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Login;


namespace Sistema_Gestor_De_Usuarios.Endpoints
{
    public static class LoginEndpoint
    {
        public static RouteGroupBuilder MapLoginEndpoint(this RouteGroupBuilder group)
        {

            group.MapPost("/", LoginAsync)
                .AddEndpointFilter<ValidationBehavior<LoginCommand>>()
                .WithOpenApi(opt =>
                {
                    opt.Summary = "Iniciar sesión";
                    opt.Description = "Este endpoint crea un nuevo cliente en el sistema, usando los datos proporcionados en el cuerpo de la solicitud.";
                    return opt;
                });

            return group;
        }

        public static async Task<Results<Ok<Result<RegisterAuthenticationResponseDto>>,
            BadRequest<Result<RegisterAuthenticationResponseDto>>>> LoginAsync
        (
            LoginCommand command,
            ISender sender)
        {
            var result = await sender.Send(command);
            if (!result.IsSuccess) return TypedResults.BadRequest(result);
            return TypedResults.Ok(result);
        }
    }
}
