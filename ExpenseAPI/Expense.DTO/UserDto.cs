
namespace Expense.DTO
{
    /// <summary>
    /// User DTO
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        public int IdUsr { get; set; }
        /// <summary>
        /// Id of the foreign key of the currency
        /// </summary>
        public int IdCur { get; set; }
        /// <summary>
        /// Name of the user
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Surname of the user
        /// </summary>
        public string? Surname { get; set; }
        /// <summary>
        /// Foreign entity: Expenses of the user
        /// </summary>
        public ICollection<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();
        /// <summary>
        /// Foreign entity: Currency of the user
        /// </summary>
        public CurrencyDto? Currency { get; set; }
    }
}
