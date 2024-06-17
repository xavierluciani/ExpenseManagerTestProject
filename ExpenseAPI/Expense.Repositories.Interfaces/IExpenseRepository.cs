
namespace Expense.Repositories.Interfaces
{
    using Expense.Common.Enum;
    using Expense.Entities.Models;

    /// <summary>
    /// Expense repository interface
    /// </summary>
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        /// <summary>
        /// Get expenses by user.
        /// </summary>
        /// <param name="userId">Current user id</param>
        /// <param name="sort">Sort method (amount / date)</param>
        /// <returns>Collection of expenses of the user</returns>
        Task<IEnumerable<Expense>> GetExpensesByUser(int userId, SortExpense sort);
        /// <summary>
        /// Verify if the expense exists in database.
        /// </summary>
        /// <param name="amount">Amount of the expense</param>
        /// <param name="date">Date of the expense</param>
        /// <returns>Result of the operation</returns>
        Task<bool> IsExpenseExists(decimal amount, DateTime date);
    }
}
