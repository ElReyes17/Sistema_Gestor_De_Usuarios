using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.User;
using Sistema_Gestor_De_Usuarios.Core.Domain.Entities;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Services
{
    public interface IUserTokenService
    {
        Task<string> BuildTokenAsync(User user);
    }
}
