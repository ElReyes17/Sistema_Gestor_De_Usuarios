using FluentValidation;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private string PasswordRegex = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{6,}$";
        public UpdateUserCommandValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId cannot be null.")
            .NotEmpty().WithMessage("UserId is required.")
            .MustAsync(async (userId, cancellationToken) => await userRepository.ExistUserAsync(userId))
            .WithMessage("UserId does not exist.");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name cannot be null.")
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Password)
            .NotNull().WithMessage("Password cannot be null.")
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(PasswordRegex).WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number.");


        }
    }
}
