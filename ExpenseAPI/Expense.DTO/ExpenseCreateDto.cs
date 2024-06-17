
namespace Expense.DTO
{
    public class ExpenseCreateDto
    {
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
        public DateTime ExpDate { get; set; }
        /// <summary>
        /// Amount of the expense
        /// </summary>
        public decimal ExpAmount { get; set; }
        /// <summary>
        /// Commentary of the expense
        /// </summary>
        public string? ExpCommentary { get; set; }
    }
}
