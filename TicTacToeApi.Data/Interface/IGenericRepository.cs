
using System.Linq.Expressions;

namespace TicTacToeApi.Data.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Update(TEntity entity);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entity);
        Task SaveAsync();
        void Save();
        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true);
    }
}
