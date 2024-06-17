
namespace Expense.Repositories
{
    using Expense.Entities.Models;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Currency repository
    /// </summary>
    public class CurrencyRepository : GenericRepository<Currency>
    {
        /// <summary>
        /// Constructof of <see cref="CurrencyRepository"/>.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="logger">Logger</param>
        public CurrencyRepository(DBBContext context, ILogger logger)
            : base(context, logger)
        {
        }
    }
}
