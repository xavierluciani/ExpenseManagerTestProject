
namespace Expense.Repositories.Interfaces
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>Collection of entities</returns>
        Task<IEnumerable<TEntity>> All();
        /// <summary>
        /// Get an entity by id.
        /// </summary>
        /// <param name="id">CUrrent id</param>
        /// <returns>Entity asked</returns>
        Task<TEntity> GetById(int id);
        /// <summary>
        /// Add a new entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result of the operation</returns>
        Task<bool> Add(TEntity entity);
        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="id">Id of the entity to delete</param>
        /// <returns>Result of the operation</returns>
        Task<bool> Delete(int id);
    }
}
