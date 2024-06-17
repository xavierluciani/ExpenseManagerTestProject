
namespace Expense.Queries
{
    using Expense.Common.Enum;
    using Expense.DTO;
    using MediatR;

    /// <summary>
    /// Query for getting expenses by user.
    /// </summary>
    /// <param name="Id">User id</param>
    /// <param name="Sort">Sort method</param>
    public record QueryAllExpenses(int Id, SortExpense Sort) : IRequest<IEnumerable<ExpenseDto>>;
}
