using Microsoft.EntityFrameworkCore;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories;
using Sistema_Gestor_De_Usuarios.Core.Domain.Entities;
using Sistema_Gestor_De_Usuarios.Infrastructure.Persistence.Context;

namespace Sistema_Gestor_De_Usuarios.Infrastructure.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        //Queries

        public async Task<List<User>> GetAllUserAsync()
           =>  await context.Users.Include(u => u.Phones).ToListAsync();
        public async Task<User> GetUserByIdAsync(Guid userId)
           =>  await context.Users.Include(u => u.Phones)
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(u => u.UserId == userId);

        public async Task<User> GetUserByEmailAsync(string email)
            => await context.Users.Include(u => u.Phones)
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<bool> ExistUserEmailAsync(string email)
           => await context.Users.Where(u => u.Email == email).AnyAsync();    

        public async Task<bool> ExistUserAsync(Guid userId)
           => await context.Users.Where(u => u.UserId == userId).AnyAsync();

        public async Task<bool> IsUniqueEmailAsync(Guid userId, string email)
        {
            var existingUser = await GetUserByEmailAsync(email);
            if (existingUser is null) return true;
            return existingUser.UserId == userId;
        }

        //Commands
        public async Task<User> AddUserAsync(User user)
        {
            await context.Set<User>().AddAsync(user);
            return user;
        }
            
        public async Task UpdateUserAsync(User user)
            => context.Set<User>().Update(user);
        
        public async Task<bool> DeleteUserAsync(User user)
        {
            if (user != null)
            {
                user.IsActive = false;
                context.Set<User>().Update(user);
                return true;
            }

            return false;
        }

        
    }
}
