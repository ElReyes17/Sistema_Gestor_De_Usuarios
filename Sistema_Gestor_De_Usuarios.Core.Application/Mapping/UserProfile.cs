using AutoMapper;
using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.Phone;
using Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Create;
using Sistema_Gestor_De_Usuarios.Core.Application.Features.User.Command.Update;
using Sistema_Gestor_De_Usuarios.Core.Domain.Entities;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            //Create
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.Phones));

            CreateMap<CreatePhoneDto, Phone>();


            //Update
            CreateMap<UpdateUserCommand, User>();

            CreateMap<PhoneDto, Phone>();
        }
    }
}
