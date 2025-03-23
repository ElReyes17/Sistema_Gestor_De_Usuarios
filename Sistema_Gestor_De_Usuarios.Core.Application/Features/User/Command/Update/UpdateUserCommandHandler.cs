

using AutoMapper;
using MediatR;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.UnitOfWork;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;
using Sistema_Gestor_De_Usuarios.Core.Application.Helper;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Update
{
    public class UpdateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
        ) : IRequestHandler<UpdateUserCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userDb = await userRepository.GetUserByIdAsync(request.UserId);

            var updatedUser = mapper.Map(request, userDb);
            var passWordHash = EncryptHash256.EncryptPassword(updatedUser.Password);
            updatedUser.Password = passWordHash;
            updatedUser.Modified = DateTime.UtcNow;

            await userRepository.UpdateUserAsync(updatedUser);
            await unitOfWork.CompleteAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
