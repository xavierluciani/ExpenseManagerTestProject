
namespace Expense.DTO
{
    /// <summary>
    /// Currency DTO
    /// </summary>
    public class CurrencyDto
    {
        /// <summary>
        /// Id of the currency
        /// </summary>
        public int IdCur { get; set; }

        /// <summary>
        /// Code of the currency
        /// </summary>
        public string? CurCode { get; set; }

        /// <summary>
        /// Name of the currency
        /// </summary>
        public string? CurName { get; set; }
    }
}
