using FluentValidation;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Delete
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId cannot be null.")
            .NotEmpty().WithMessage("UserId is required.")
            .MustAsync(async (userId, cancellationToken) => await userRepository.ExistUserAsync(userId))
            .WithMessage("UserId does not exist.");
        }
    }
}
