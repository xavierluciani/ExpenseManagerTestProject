
namespace Expense.DTO
{
    /// <summary>
    /// Expense DTO
    /// </summary>
    public class ExpenseDto
    {
        /// <summary>
        /// Id of the expense
        /// </summary>
        public int IdExp { get; set; }
        /// <summary>
        /// Id of the foreign key nature
        /// </summary>
        public int IdNat { get; set; }
        /// <summary>
        /// Id of the foreign key user
        /// </summary>
        public int IdUsr { get; set; }
        /// <summary>
        /// Date of the expense
        /// </summary>
        public DateTime? ExpDate { get; set; }
        /// <summary>
        /// Amount of the expense
        /// </summary>
        public decimal? ExpAmount { get; set; }
        /// <summary>
        /// Commentary of the expense
        /// </summary>
        public string? ExpCommentary { get; set; }
        /// <summary>
        /// Foreign entity: nature of the expense
        /// </summary>
        public NatureDto? Nature { get; set; }
        /// <summary>
        /// User name of the owner of the expense
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// Currency name of the expense
        /// </summary>
        public string? Currency { get; set; }
    }
}
