using Microsoft.EntityFrameworkCore;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.UnitOfWork;
using Sistema_Gestor_De_Usuarios.Infrastructure.Persistence.Context;

namespace Sistema_Gestor_De_Usuarios.Infrastructure.Persistence
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }
        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (var list in entities) context.Set<TEntity>().Entry(list).State = EntityState.Deleted;
            context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
