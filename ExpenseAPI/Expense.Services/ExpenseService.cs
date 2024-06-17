
namespace Expense.Services
{
    using Expense.Repositories.Interfaces;
    using Expense.Services.Interfaces;
    using Expense.DTO;
    using AutoMapper;
    using Expense.Entities.Models;
    using Expense.Repositories;
    using Expense.Common.Enum;

    /// <summary>
    /// Expense service
    /// </summary>
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor of <see cref="ExpenseService"/>.
        /// </summary>
        /// <param name="repository">Expense repository</param>
        /// <param name="mapper">Automapper</param>
        public ExpenseService(IExpenseRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Add a new expense in database.
        /// </summary>
        /// <param name="expenseDto">Current expense</param>
        /// <returns>Result of the operation</returns>
        public async Task<bool> AddExpense(ExpenseDto expenseDto)
        {
            var entity = this._mapper.Map<Expense>(expenseDto);

            return await ((ExpenseRepository)this._repository).Add(entity);
        }

        /// <summary>
        /// Get expenses according to an user.
        /// </summary>
        /// <param name="userId">Current user id</param>
        /// <param name="sort">Sort method</param>
        /// <returns>Collection of expenses of the user</returns>
        public async Task<IEnumerable<ExpenseDto>>GetExpensesByUser(int userId, SortExpense sort)
        {
            var expenses = await this._repository.GetExpensesByUser(userId, sort);
            var dtos = expenses.Select(this._mapper.Map<ExpenseDto>);

            return dtos;
        }

        /// <summary>
        /// Verify if the expense exsists in database.
        /// </summary>
        /// <param name="amount">Amount of the expense</param>
        /// <param name="date">Date of the expense</param>
        /// <returns>Result of the operation</returns>
        public async Task<bool> IsExpenseExists(decimal amount, DateTime date)
        {
            return await this._repository.IsExpenseExists(amount, date);
        }
    }
}
