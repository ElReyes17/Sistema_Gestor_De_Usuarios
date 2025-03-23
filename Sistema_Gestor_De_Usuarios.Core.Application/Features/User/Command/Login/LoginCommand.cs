using MediatR;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.User;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Login
{
    public class LoginCommand : IRequest<Result<RegisterAuthenticationResponseDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
