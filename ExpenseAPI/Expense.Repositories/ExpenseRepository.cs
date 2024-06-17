
namespace Expense.Repositories
{
    using Expense.Common.Enum;
    using Expense.Entities.Models;
    using Expense.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Expense repository
    /// </summary>
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        /// <summary>
        /// Constructor of <see cref="ExpenseRepository"/>.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="logger">Logger</param>
        public ExpenseRepository(DBBContext context, ILogger logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// Get expenses by user.
        /// </summary>
        /// <param name="userId">Current user id</param>
        /// <param name="sort">Sort method (amount / date)</param>
        /// <returns>Collection of expenses of the user</returns>
        public async Task<IEnumerable<Expense>> GetExpensesByUser(int userId, SortExpense sort)
        {
            List<Expense> expenses = new();

            try
            {
                Expression<Func<Expense, object>> order;
                if (sort == SortExpense.Amount)
                {
                    order = o => o.ExpAmount!;
                }
                else
                {
                    order = o => o.ExpDate!;
                }

                expenses = await this._dbSet
                    .Include(o => o.Nature)
                    .Include(o => o.User)
                    .ThenInclude(o => o.Currency)
                    .Where(x => x.User.IdUsr == userId).OrderBy(order).ToListAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error while getting expenses of user {userId}", userId);
                this._context.Dispose();
            }

            return expenses;
        }
        /// <summary>
        /// Verify if the expense exists in database.
        /// </summary>
        /// <param name="amount">Amount of the expense</param>
        /// <param name="date">Date of the expense</param>
        /// <returns>Result of the operation</returns>
        public async Task<bool> IsExpenseExists(decimal amount, DateTime date)
        {
            return await this._dbSet.AnyAsync(x => x.ExpAmount == amount && x.ExpDate == date);
        }
    }
}
