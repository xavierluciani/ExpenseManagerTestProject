
namespace Expense.Repositories
{
    using Expense.Entities.Models;
    using Expense.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// User repository
    /// </summary>
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        /// <summary>
        /// Constructof of <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="logger">Logger</param>
        public UserRepository(DBBContext context, ILogger logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// Verify if the user exists in database.
        /// </summary>
        /// <param name="userId">Current user id</param>
        /// <returns>Result of the operation</returns>
        public async Task<bool> IsUserExists(int userId)
        {
            return await this._dbSet.AnyAsync(x => x.IdUsr == userId);
        }
    }
}
