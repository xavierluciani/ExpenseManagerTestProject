
namespace Expense.QueryHandlers
{
    using Expense.DTO;
    using Expense.Queries;
    using Expense.Services.Interfaces;
    using MediatR;

    /// <summary>
    /// Handler query class for the querying expenses of an user.
    /// </summary>
    public class QueryAllExpensesHandler : IRequestHandler<QueryAllExpenses, IEnumerable<ExpenseDto>>
    {
        private readonly IExpenseService _expenseService;

        /// <summary>
        /// Constructor of <see cref="QueryAllExpensesHandler"/>.
        /// </summary>
        /// <param name="expenseService">Expense service</param>
        public QueryAllExpensesHandler(IExpenseService expenseService)
        {
            this._expenseService = expenseService;
        }

        /// <summary>
        /// Handler method for getting expenses of an user.
        /// </summary>
        /// <param name="request">Current request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Collection of expenses of the user</returns>
        public async Task<IEnumerable<ExpenseDto>> Handle(QueryAllExpenses request, CancellationToken cancellationToken)
        {
            var expenses = await this._expenseService.GetExpensesByUser(request.Id, request.Sort);

            return expenses;
        }
    }
}
