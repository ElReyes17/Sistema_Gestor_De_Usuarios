
using MediatR;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Delete
{
    public class DeleteUserCommand : IRequest<Result<bool>>
    {
        public Guid UserId { get; set; } 
    }
}
