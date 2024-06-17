
namespace Expense.Repositories
{
    using Expense.Entities.Models;
    using Expense.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class NatureRepository : GenericRepository<Nature>, INatureRepository
    {
        /// <summary>
        /// Constructof of <see cref="NatureRepository"/>.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="logger">Logger</param>
        public NatureRepository(DBBContext context, ILogger logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// Verify if the nature exists in database.
        /// </summary>
        /// <param name="natureId">Current nature id</param>
        /// <returns>Result of the operation</returns>
        public async Task<bool> IsNatureExists(int natureId)
        {
            return await this._dbSet.AnyAsync(x => x.IdNat == natureId);
        }
    }
}
