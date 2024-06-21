
namespace Expense.API.Controllers
{
    using Expense.Commands;
    using Expense.Common.Enum;
    using Expense.Common.Exception;
    using Expense.DTO;
    using Expense.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public ExpenseController(IMediator mediatr)
        {
            this._mediatr = mediatr;
        }

        /// <summary>
        /// Get expenses according to a user id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="sort">Sort method (date / amount)</param>
        /// <returns>Expenses of the user asked</returns>
        [HttpGet]
        [Route("GetAllUserExpenses/{userId}/{sort}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<ExpenseDto>), 200)]
        public async Task<IActionResult> GetAllUserExpenses(int userId, SortExpense sort)
        {
            var results = await this._mediatr.Send(new QueryAllExpenses(userId, sort));

            if (!results.Any())
            {
                return NoContent();
            }
            else
            {
                return Ok(results);
            }
        }

        /// <summary>
        /// Create a new expense
        /// </summary>
        /// <param name="idUser">Id of the user asscoiated to the expense</param>
        /// <param name="idNature">Id of the nature for the expense</param>
        /// <param name="amount">Amount of the expense</param>
        /// <param name="date">Date of the expense</param>
        /// <param name="commentary">Commentary for the expense</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("CreateExpense")]
        public async Task<IActionResult> Create([FromBody] ExpenseCreateDto expense)
        {
            try
            {
                var result = await this._mediatr.Send(new CommandCreateExpense(expense));
                return Created(string.Empty, result);
            }
            catch (BadRequestWebException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(NotFoundWebException ex)
            {
                return NotFound(ex.Message);
            }
            catch(ConflictWebException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
