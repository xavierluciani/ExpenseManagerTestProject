
namespace Expense.Repositories
{
    using Expense.Entities.Models;
    using Expense.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Generic repository for any entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        protected DBBContext _context;
        protected DbSet<TEntity> _dbSet;
        protected readonly ILogger _logger;

        /// <summary>
        /// Constructor of <see cref="GenericRepository"/>.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="logger">Logger</param>
        public GenericRepository(
            DBBContext context,
            ILogger logger
        )
        {
            this._context = context;
            this._logger = logger;
            this._dbSet = this._context.Set<TEntity>();
        }

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>Collection of entities</returns>
        public virtual async Task<IEnumerable<TEntity>> All()
        {
            return await this._dbSet.ToListAsync();
        }
        /// <summary>
        /// Get an entity by id.
        /// </summary>
        /// <param name="id">CUrrent id</param>
        /// <returns>Entity asked</returns>
        public virtual async Task<TEntity?> GetById(int id)
        {
            try
            {
                return await this._dbSet.FindAsync(id);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Error getting entity with id {Id}", id);
                return null!;
            }
        }
        /// <summary>
        /// Add a new entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result of the operation</returns>
        public virtual async Task<bool> Add(TEntity entity)
        {
            try
            {
                await this._dbSet.AddAsync(entity);
                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Error adding entity");
                return false;
            }
        }
        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="id">Id of the entity to delete</param>
        /// <returns>Result of the operation</returns>
        public virtual async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await this._dbSet.FindAsync(id);
                if (entity != null)
                {
                    this._dbSet.Remove(entity);
                    await this._context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    this._logger.LogWarning("Entity with id {Id} not found for deletion", id);
                    return false;
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "Error deleting entity with id {Id}", id);
                return false;
            }
        }
    }
}
