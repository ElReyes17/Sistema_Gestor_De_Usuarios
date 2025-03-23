using AutoMapper;
using MediatR;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Services;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.UnitOfWork;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.Phone;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.User;
using Sistema_Gestor_De_Usuarios.Core.Application.Helper;
using Sistema_Gestor_De_Usuarios.Core.Domain.Entities;


namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Create
{
    public class CreateUserCommandHandler(
        IUserRepository userRepository,
        IUserTokenService userTokenService,
        IUnitOfWork unitOfWork,
        IMapper mapper
        ) : IRequestHandler<CreateUserCommand, Result<RegisterAuthenticationResponseDto>>
    {
        public async Task<Result<RegisterAuthenticationResponseDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Mapeamos el nuevo usuario
            var newUser = mapper.Map<Domain.Entities.User>(request);

            //Creamos el token
            var token = await userTokenService.BuildTokenAsync(newUser);

            //Encriptamos la password
            var passWordHash = EncryptHash256.EncryptPassword(newUser.Password);
            newUser.Password = passWordHash;
            newUser.IsActive = true;
            newUser.LastLogin = DateTime.UtcNow;
            newUser.Token = token;

            //Agregamos el nuevo usuario en Db
            await userRepository.AddUserAsync(newUser);
            await unitOfWork.CompleteAsync();

            //Se mapea la respuesta
            var userPhones = newUser.Phones
            .Select(phone => new PhoneDto
            {
              PhoneId = phone.PhoneId,
              Number = phone.Number,
              CityCode = phone.CityCode,
              CountryCode = phone.CountryCode

            }).ToList();

            var response = new RegisterAuthenticationResponseDto
            {
                UserId = newUser.UserId,
                Name = newUser.Name,
                Email = newUser.Email,
                Created = newUser.Created,
                Modified = newUser.Modified,
                LastLogin = newUser.LastLogin,
                Token = token,
                IsActive = newUser.IsActive,
                Phones = userPhones
            };
            return Result<RegisterAuthenticationResponseDto>.Success(response);
        }
    }
}
