

using Sistema_Gestor_De_Usuarios.Core.Domain.Entities;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories
{
    public interface IUserRepository
    {
        //Queries
        Task<List<User>> GetAllUserAsync();
        Task<User> GetUserByIdAsync(Guid userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> ExistUserAsync(Guid userId);
        Task<bool> ExistUserEmailAsync(string email);
        Task<bool> IsUniqueEmailAsync(Guid userId, string email);

        //Commands
        Task<User> AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
    }
}
