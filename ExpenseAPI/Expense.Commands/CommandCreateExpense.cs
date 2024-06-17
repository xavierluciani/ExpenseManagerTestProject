using Expense.DTO;
using MediatR;

namespace Expense.Commands
{
    /// <summary>
    /// Command for the expense creation.
    /// </summary>
    /// <param name="UserId">Id of the nature for the expense</param>
    /// <param name="NatureId">Id of the nature for the expense</param>
    /// <param name="Amount">Amount of the expense</param>
    /// <param name="Date">Date of the expense</param>
    /// <param name="Commentary">Commentary for the expense</param>
    public record CommandCreateExpense(ExpenseCreateDto Expense) : IRequest<bool>;
}
