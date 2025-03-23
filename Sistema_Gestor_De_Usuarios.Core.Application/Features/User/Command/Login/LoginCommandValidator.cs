


using FluentValidation;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email cannot be null.")
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(async (email, cancellationToken) =>
                  await userRepository.ExistUserEmailAsync(email))
                .WithMessage("The email does not exist.");

            RuleFor(x => x.Password)
            .NotNull().WithMessage("Password cannot be null.")
            .NotEmpty().WithMessage("Password is required.");

        }
    }
}
