

using FluentValidation;
using Microsoft.Extensions.Options;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories;
using Sistema_Gestor_De_Usuarios.Options;
using System.Text.RegularExpressions;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {

        private readonly RegexOption _regexOption;

        public CreateUserCommandValidator(IUserRepository userRepository, IOptions<RegexOption> options)
        {

             _regexOption = options.Value;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(async (email, cancellationToken) =>
                   !await userRepository.ExistUserEmailAsync(email))
                .WithMessage("The email provided is already in use by another user.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(_regexOption.PasswordRegex).WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number and special caracter");


            RuleForEach(x => x.Phones)
            .ChildRules(phones =>
            {

                phones.RuleFor(p => p.Number)
                     .NotNull().WithMessage("Number cannot be null.")
                     .NotEmpty().WithMessage("Phone number is required.")
                     .Matches(_regexOption.PhoneRegex).WithMessage("Invalid phone number format.");

                phones.RuleFor(x => x.CityCode)
                      .NotNull().WithMessage("CityCode cannot be null.")
                         .NotEmpty().WithMessage("City code is required.")
                         .Matches(_regexOption.CityCode).WithMessage("City code should only contain numbers.");

                phones.RuleFor(x => x.CountryCode)
                      .NotNull().WithMessage("CountryCode cannot be null.")
                      .NotEmpty().WithMessage("Country is required.")
                      .Matches(_regexOption.CountryCode).WithMessage("Country code should be a valid number (1-4 digits).");
            });
        }
    }
}
