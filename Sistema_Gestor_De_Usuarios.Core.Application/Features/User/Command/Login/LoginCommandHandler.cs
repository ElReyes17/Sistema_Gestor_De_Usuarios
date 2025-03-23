using MediatR;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Services;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.UnitOfWork;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.Phone;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.User;
using Sistema_Gestor_De_Usuarios.Core.Application.Helper;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Login
{
    public class LoginCommandHandler(
        IUserRepository userRepository,
        IUserTokenService userTokenService,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<LoginCommand, Result<RegisterAuthenticationResponseDto>>
    {
        public async Task<Result<RegisterAuthenticationResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userDb = await userRepository.GetUserByEmailAsync(request.Email);

            string verifyPassword = EncryptHash256.EncryptPassword(request.Password);

            if (userDb.Password != verifyPassword) throw new ApiException("The password is incorrect", 400);

            var token = await userTokenService.BuildTokenAsync(userDb);
            userDb.Token = token;
            userDb.LastLogin = DateTime.UtcNow;
            userDb.Modified = DateTime.UtcNow;

            await userRepository.UpdateUserAsync(userDb);
            await unitOfWork.CompleteAsync();

            var userPhones = userDb.Phones
            .Select(phone => new PhoneDto
            {
                PhoneId = phone.PhoneId,
                Number = phone.Number,
                CityCode = phone.CityCode,
                CountryCode = phone.CountryCode

            }).ToList();

            var response = new RegisterAuthenticationResponseDto
            {
                UserId = userDb.UserId,
                Name = userDb.Name,
                Email = userDb.Email,
                Created = userDb.Created,
                Modified = userDb.Modified,
                LastLogin = userDb.LastLogin,
                Token = token,
                IsActive = userDb.IsActive,
                Phones = userPhones
            };
            return Result<RegisterAuthenticationResponseDto>.Success(response);

        }
    }
}
