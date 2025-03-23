
using MediatR;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.User;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Query.GetAll
{
    public class GetAllUserQuery : IRequest<Result<IEnumerable<UserDto>>>;
}
