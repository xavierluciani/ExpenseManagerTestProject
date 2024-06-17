
namespace Expense.CommandHandlers
{
    using Expense.Commands;
    using MediatR;
    using Expense.Common.Exception;
    using Expense.Services.Interfaces;
    using Expense.DTO;

    /// <summary>
    /// Handler command class for the expense creation.
    /// </summary>
    public class CommandCreateExpenseHandler : IRequestHandler<CommandCreateExpense, bool>
    {
        private readonly IExpenseService _expenseService;
        private readonly IUserService _userService;
        private readonly INatureService _natureService;

        /// <summary>
        /// Constructor of <see cref="CommandCreateExpenseHandler"/>.
        /// </summary>
        /// <param name="expenseService">Expense service</param>
        /// <param name="userService">User service</param>
        /// <param name="natureService">Nature service</param>
        public CommandCreateExpenseHandler(IExpenseService expenseService, IUserService userService, INatureService natureService)
        {
            this._expenseService = expenseService;
            this._userService = userService;
            this._natureService = natureService;
        }

        /// <summary>
        /// Handler method for expense creation.
        /// </summary>
        /// <param name="request">Current request</param>
        /// <param name="cancellationToken">Cancellationt token</param>
        /// <returns>Result of the operation</returns>
        public async Task<bool> Handle(CommandCreateExpense request, CancellationToken cancellationToken)
        {
            this.CheckMandatoryCommentary(request.Expense.ExpCommentary ?? string.Empty);
            this.IsDateInFuture(request.Expense.ExpDate);
            this.IsDateOlderThanThreeMonth(request.Expense.ExpDate);
            await this.IsUserExists(request.Expense.IdUsr);
            await this.IsNatureExists(request.Expense.IdNat);
            await this.IsExpenseAlreadyDeclared(request.Expense.ExpAmount, request.Expense.ExpDate);

            var expenseDto = new ExpenseDto()
            {
                IdUsr = request.Expense.IdUsr,
                IdNat = request.Expense.IdNat,
                ExpAmount = request.Expense.ExpAmount,
                ExpDate = request.Expense.ExpDate,
                ExpCommentary = request.Expense.ExpCommentary,
            };

            return await this._expenseService.AddExpense(expenseDto);
        }

        /// <summary>
        /// Verify that commentary is filled.
        /// </summary>
        /// <param name="commentary">Current commentary</param>
        /// <exception cref="BadRequestWebException">Commentary not filled</exception>
        private void CheckMandatoryCommentary(string commentary)
        {
            if (string.IsNullOrWhiteSpace(commentary))
            {
                throw new BadRequestWebException("Commentary is mandatory.");
            }
        }

        /// <summary>
        /// Check that date is not the future.
        /// </summary>
        /// <param name="date">Current date</param>
        /// <exception cref="BadRequestWebException">Date is later than date now</exception>
        private void IsDateInFuture(DateTime date)
        {
            if (date > DateTime.Now)
            {
                throw new BadRequestWebException("Expense date must not be in the future.");
            }
        }

        /// <summary>
        /// Check that date is not older than three months.
        /// </summary>
        /// <param name="date">Current date</param>
        /// <exception cref="BadRequestWebException">Date is older than three months</exception>
        private void IsDateOlderThanThreeMonth(DateTime date)
        {
            if (date.AddMonths(3) < DateTime.Now)
            {
                throw new BadRequestWebException("Expense must not be older than three months.");
            }
        }

        /// <summary>
        /// Check that there is no existing expense.
        /// </summary>
        /// <param name="amount">Current amount</param>
        /// <param name="date">Current date</param>
        /// <exception cref="ConflictWebException">Another expense already exists for the current expense</exception>
        private async Task IsExpenseAlreadyDeclared(decimal amount, DateTime date)
        {
            if (await this._expenseService.IsExpenseExists(amount, date))
            {
                throw new ConflictWebException("Expense already exists in database.");
            }
        }

        /// <summary>
        /// Check that the user asked exists in database.
        /// </summary>
        /// <param name="userId">Current user id</param>
        /// <exception cref="NotFoundWebException">User does not exists in database</exception>
        private async Task IsUserExists(int userId)
        {
            if (!await this._userService.IsUserExists(userId))
            {
                throw new NotFoundWebException($"User {userId} not found.");
            }
        }

        /// <summary>
        /// Check that the nature asked exists in database.
        /// </summary>
        /// <param name="natureId">Current nature id</param>
        /// <exception cref="BadRequestWebException">Nature does not exists in database</exception>
        private async Task IsNatureExists(int natureId)
        {
            if (!await this._natureService.IsNatureExists(natureId))
            {
                throw new BadRequestWebException($"Nature {natureId} not found.");
            }
        }
    }
}
