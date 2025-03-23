using MediatR;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.Phone;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.User;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Create
{
    public class CreateUserCommand : IRequest<Result<RegisterAuthenticationResponseDto>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }        
        public ICollection<CreatePhoneDto> Phones { get; set; }
    }
}
