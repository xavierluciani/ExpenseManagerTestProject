namespace Expense.Services.Interfaces
{
    using Expense.Common.Enum;
    using Expense.DTO;

    /// <summary>
    /// Expense service interface
    /// </summary>
    public interface IExpenseService
    {
        /// <summary>
        /// Add a new expense in database.
        /// </summary>
        /// <param name="expenseDto">Current expense</param>
        /// <returns>Result of the operation</returns>
        Task<bool> AddExpense(ExpenseDto expenseDto);
        /// <summary>
        /// Get expenses according to an user.
        /// </summary>
        /// <param name="userId">Current user id</param>
        /// <param name="sort">Sort method</param>
        /// <returns>Collection of expenses of the user</returns>
        Task<IEnumerable<ExpenseDto>>GetExpensesByUser(int userId, SortExpense sort);
        /// <summary>
        /// Verify if the expense exsists in database.
        /// </summary>
        /// <param name="amount">Amount of the expense</param>
        /// <param name="date">Date of the expense</param>
        /// <returns>Result of the operation</returns>
        Task<bool> IsExpenseExists(decimal amount, DateTime date);
    }
}
