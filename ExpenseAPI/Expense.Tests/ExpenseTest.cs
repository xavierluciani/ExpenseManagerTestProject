namespace Expense.Tests
{
    using Expense.CommandHandlers;
    using Expense.Commands;
    using Expense.Common.Enum;
    using Expense.Common.Exception;
    using Expense.DTO;
    using Expense.Queries;
    using Expense.QueryHandlers;
    using Expense.Services.Interfaces;
    using MediatR;
    using Moq;
    using System;

    [TestClass]
    public class ExpenseTest
    {
        private readonly Mock<IExpenseService> expenseService = new();
        private readonly Mock<IUserService> userService = new();
        private readonly Mock<INatureService> natureService = new();

        /// <summary>
        /// Test method to get expenses.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetExpensesHandler()
        {
            QueryAllExpenses query = new QueryAllExpenses(It.IsAny<int>(), It.IsAny<SortExpense>());
            expenseService.Setup(s => s.GetExpensesByUser(It.IsAny<int>(), It.IsAny<SortExpense>()))
                .ReturnsAsync(new List<ExpenseDto>()
                {
                    new ExpenseDto(),
                    new ExpenseDto()
                });

            QueryAllExpensesHandler handler = new(expenseService.Object);
            var results = await handler.Handle(query, It.IsAny<CancellationToken>());

            Assert.AreEqual(2, results.Count());
        }

        /// <summary>
        /// Test method to create an expense.
        /// </summary>
        [TestMethod]
        public async Task CreateExpenseHandler()
        {
            var expenseCreateDto = new ExpenseCreateDto() { IdUsr = It.IsAny<int>(), IdNat = It.IsAny<int>(), ExpAmount = 100, ExpDate = DateTime.Now.AddDays(-1), ExpCommentary = "Test" };
            CommandCreateExpense command = new CommandCreateExpense(expenseCreateDto);
            userService.Setup(u => u.IsUserExists(It.IsAny<int>())).ReturnsAsync(true);
            natureService.Setup(n => n.IsNatureExists(It.IsAny<int>())).ReturnsAsync(true);
            expenseService.Setup(s => s.IsExpenseExists(It.IsAny<decimal>(), It.IsAny<DateTime>())).ReturnsAsync(false);
            expenseService.Setup(s => s.AddExpense(It.IsAny<ExpenseDto>())).ReturnsAsync(true);

            CommandCreateExpenseHandler handler = new(expenseService.Object, userService.Object, natureService.Object);
            var result = await handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test method to check the failure because expense already exists.
        /// </summary>
        [TestMethod]
        public async Task CreateExpenseFailExpenseAlreadyExsists()
        {
            var expenseCreateDto = new ExpenseCreateDto() { IdUsr = It.IsAny<int>(), IdNat = It.IsAny<int>(), ExpAmount = 100, ExpDate = DateTime.Now.AddDays(-1), ExpCommentary = "Test" };
            CommandCreateExpense command = new CommandCreateExpense(expenseCreateDto);
            userService.Setup(u => u.IsUserExists(It.IsAny<int>())).ReturnsAsync(true);
            natureService.Setup(n => n.IsNatureExists(It.IsAny<int>())).ReturnsAsync(true);
            expenseService.Setup(s => s.IsExpenseExists(It.IsAny<decimal>(), It.IsAny<DateTime>())).ReturnsAsync(true);

            CommandCreateExpenseHandler handler = new(expenseService.Object, userService.Object, natureService.Object);

            await AssertException<ConflictWebException, CommandCreateExpense>(handler.Handle, command, It.IsAny<CancellationToken>());
        }

        /// <summary>
        /// Test method to check the failure because the user is not found.
        /// </summary>
        [TestMethod]
        public async Task CreateExpenseFailUserNotFound()
        {
            var expenseCreateDto = new ExpenseCreateDto() { IdUsr = It.IsAny<int>(), IdNat = It.IsAny<int>(), ExpAmount = 100, ExpDate = DateTime.Now.AddDays(-1), ExpCommentary = "Test" };
            CommandCreateExpense command = new CommandCreateExpense(expenseCreateDto);
            userService.Setup(u => u.IsUserExists(It.IsAny<int>())).ReturnsAsync(false);
            natureService.Setup(n => n.IsNatureExists(It.IsAny<int>())).ReturnsAsync(true);
            expenseService.Setup(s => s.IsExpenseExists(It.IsAny<decimal>(), It.IsAny<DateTime>())).ReturnsAsync(false);

            CommandCreateExpenseHandler handler = new(expenseService.Object, userService.Object, natureService.Object);
            await AssertException<NotFoundWebException, CommandCreateExpense>(handler.Handle, command, It.IsAny<CancellationToken>());
        }

        /// <summary>
        /// Test method to check the failure because the nature of the expense is not found.
        /// </summary>
        [TestMethod]
        public async Task CreateExpenseFailNatureNotFound()
        {
            var expenseCreateDto = new ExpenseCreateDto() { IdUsr = It.IsAny<int>(), IdNat = It.IsAny<int>(), ExpAmount = 100, ExpDate = DateTime.Now.AddDays(-1), ExpCommentary = "Test" };
            CommandCreateExpense command = new CommandCreateExpense(expenseCreateDto);
            userService.Setup(u => u.IsUserExists(It.IsAny<int>())).ReturnsAsync(true);
            natureService.Setup(n => n.IsNatureExists(It.IsAny<int>())).ReturnsAsync(false);
            expenseService.Setup(s => s.IsExpenseExists(It.IsAny<decimal>(), It.IsAny<DateTime>())).ReturnsAsync(true);

            CommandCreateExpenseHandler handler = new(expenseService.Object, userService.Object, natureService.Object);
            await AssertException<BadRequestWebException, CommandCreateExpense>(handler.Handle, command, It.IsAny<CancellationToken>());
        }

        /// <summary>
        /// Test method to check the failure because the expense date is in the future.
        /// </summary>
        [TestMethod]
        public async Task CreateExpenseFailFutureDate()
        {
            var expenseCreateDto = new ExpenseCreateDto() { IdUsr = It.IsAny<int>(), IdNat = It.IsAny<int>(), ExpAmount = 100, ExpDate = DateTime.Now.AddDays(1), ExpCommentary = "Test" };
            CommandCreateExpense command = new CommandCreateExpense(expenseCreateDto);

            CommandCreateExpenseHandler handler = new(expenseService.Object, userService.Object, natureService.Object);
            await AssertException<BadRequestWebException, CommandCreateExpense>(handler.Handle, command, It.IsAny<CancellationToken>());
        }

        /// <summary>
        /// Test method to check the failure because the date is older than three months.
        /// </summary>
        [TestMethod]
        public async Task CreateExpenseFailDateOlderThanThreeMonths()
        {
            var expenseCreateDto = new ExpenseCreateDto() { IdUsr = It.IsAny<int>(), IdNat = It.IsAny<int>(), ExpAmount = 100, ExpDate = DateTime.Now.AddMonths(-4), ExpCommentary = "Test" };
            CommandCreateExpense command = new CommandCreateExpense(expenseCreateDto);

            CommandCreateExpenseHandler handler = new(expenseService.Object, userService.Object, natureService.Object);
            await AssertException<BadRequestWebException, CommandCreateExpense>(handler.Handle, command, It.IsAny<CancellationToken>());
        }

        /// <summary>
        /// Test method to check the failure because the commentary is empty.
        /// </summary>
        [TestMethod]
        public async Task CreateExpenseFailCommentaryEmpty()
        {
            var expenseCreateDto = new ExpenseCreateDto() { IdUsr = It.IsAny<int>(), IdNat = It.IsAny<int>(), ExpAmount = 100, ExpDate = DateTime.Now.AddDays(1), ExpCommentary = "  " };
            CommandCreateExpense command = new CommandCreateExpense(expenseCreateDto);

            CommandCreateExpenseHandler handler = new(expenseService.Object, userService.Object, natureService.Object);
            await AssertException<BadRequestWebException, CommandCreateExpense>(handler.Handle, command, It.IsAny<CancellationToken>());
        }

        /// <summary>
        /// Handler delegate method.
        /// </summary>
        /// <typeparam name="TCommand">Command method type</typeparam>
        /// <param name="request">Request method</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        private delegate Task<bool> HandleDelegate<TCommand>(TCommand request, CancellationToken cancellationToken);

        /// <summary>
        /// Verify if the <see cref="Exception"/> is correctly thrown during the assert.
        /// </summary>
        /// <typeparam name="TException">Exception type expected</typeparam>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="handle">Handler delegate to be invoked</param>
        /// <param name="command">Command to be executed by the handler</param>
        /// <param name="token">Cancellation token</param>
        private static async Task AssertException<TException, TCommand>(HandleDelegate<TCommand> handle, TCommand command, CancellationToken token)
            where TException : Exception
            where TCommand : IRequest<bool>
        {
            await Assert.ThrowsExceptionAsync<TException>(async () => await handle.Invoke(command, token));
        }
    }
}