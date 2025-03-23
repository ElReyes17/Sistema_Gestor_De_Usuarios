
using AutoMapper;
using MediatR;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories;
using Sistema_Gestor_De_Usuarios.Core.Application.Common;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.User;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Query.GetAll
{
    public class GetAllUserQueryHandler(
        IUserRepository userRepository
        ) : IRequestHandler<GetAllUserQuery, Result<IEnumerable<UserDto>>>
    {
        public async Task<Result<IEnumerable<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var userListDb = await userRepository.GetAllUserAsync();
            var userList = userListDb.Select(list => new UserDto
            {
                Id = list.UserId,
                Name = list.Name,
                Email = list.Email,
                Phones = list.Phones.Select(phoneList => new Dtos.Phone.PhoneDto
                {
                    PhoneId = phoneList.PhoneId,
                    Number = phoneList.Number,
                    CityCode = phoneList.CityCode,
                    CountryCode = phoneList.CountryCode,
                }).ToList()

            });

            return Result<IEnumerable<UserDto>>.Success(userList);
        }
    }
}
