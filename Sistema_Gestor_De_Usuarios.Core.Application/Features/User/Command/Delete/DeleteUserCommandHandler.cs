

using MediatR;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.UnitOfWork;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Delete
{
    public class DeleteUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork     
        ) : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userDb = await userRepository.GetUserByIdAsync(request.UserId);
            if (userDb is not null)
            {
                await userRepository.DeleteUserAsync(userDb);
                await unitOfWork.CompleteAsync();
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure("Algo ha fallado intentando borrar el usuario");
        }
    }
}
