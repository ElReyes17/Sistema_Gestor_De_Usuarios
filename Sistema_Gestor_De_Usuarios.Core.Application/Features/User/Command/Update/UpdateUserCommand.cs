
using MediatR;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Update
{
    public class UpdateUserCommand : IRequest<Result<Unit>>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
}
