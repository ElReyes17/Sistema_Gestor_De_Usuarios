
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.User;
using Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Create;
using Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Delete;
using Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Update;
using Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Query.GetAll;

namespace Sistema_Gestor_De_Usuarios.Endpoints
{
    public static class UserEndpoint
    {
        public static RouteGroupBuilder MapUserEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAllUserAsync)
                .RequireAuthorization()
                .WithOpenApi(opt =>
                {
                    opt.Summary = "Obtener todos los usuarios";
                    opt.Description = "Este endpoint devuelve la lista completa de usuarios registrados en la aplicación.";
                    return opt;
                });


            group.MapPost("/", CreateUserAsync)
                .AddEndpointFilter<ValidationBehavior<CreateUserCommand>>()
                .WithOpenApi(opt =>
                {
                    opt.Summary = "Registrar un nuevo usuario";
                    opt.Description = "Este endpoint permite registrar un nuevo usuario en la aplicación.";
                    return opt;
                });

            group.MapPatch("/", UpdateUserAsync)
                .AddEndpointFilter<ValidationBehavior<UpdateUserCommand>>()
                .RequireAuthorization()
                .WithOpenApi(opt =>
                {
                    opt.Summary = "Cambiar nombre o contraseña de un usuario";
                    opt.Description = "Este endpoint actualizar el nombre o la contraseña a un usuario ya existente.";
                    return opt;
                });

            group.MapDelete("/", DeleteUserAsync)
                .AddEndpointFilter<ValidationBehavior<DeleteUserCommand>>()
                .RequireAuthorization()
                .WithOpenApi(opt =>
                {
                    opt.Summary = "Eliminar un usuario";
                    opt.Description = "Este endpoint elimina un usuario de la aplicación con base en su ID.";
                    return opt;
                });

            return group;
        }

        public static async Task<Results<Ok<Result<IEnumerable<UserDto>>>, NotFound>> GetAllUserAsync
       (
           ISender sender)
        {
            var result = await sender.Send(new GetAllUserQuery());
            return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }


        public static async Task<Results<Ok<Result<RegisterAuthenticationResponseDto>>, 
            BadRequest<Result<RegisterAuthenticationResponseDto>>>> CreateUserAsync
        (
            CreateUserCommand command,
            ISender sender)
        {
            var result = await sender.Send(command);
            if (!result.IsSuccess) return TypedResults.BadRequest(result);
            return TypedResults.Ok(result);
        }

        public static async Task<Results<NoContent, BadRequest<Result<Unit>>>> UpdateUserAsync
        (
            UpdateUserCommand command,
            ISender sender)
        {
            var result = await sender.Send(command);
            if (!result.IsSuccess) return TypedResults.BadRequest(result);
            return TypedResults.NoContent();
        }

        public static async Task<Results<NoContent, BadRequest<Result<bool>>>> DeleteUserAsync
        (
            [FromBody]
            DeleteUserCommand command,
            ISender sender)
        {
            var result = await sender.Send(command);
            if (!result.IsSuccess) return TypedResults.BadRequest(result);
            return TypedResults.NoContent();
        }
    }
}
