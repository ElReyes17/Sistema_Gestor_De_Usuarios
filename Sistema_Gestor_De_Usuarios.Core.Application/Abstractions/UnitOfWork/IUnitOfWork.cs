
namespace Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}
