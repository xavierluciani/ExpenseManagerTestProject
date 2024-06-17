
namespace Expense.Services.Interfaces
{
    /// <summary>
    /// Nature service interface
    /// </summary>
    public interface INatureService
    {
        /// <summary>
        /// Verify if the nature exists in database.
        /// </summary>
        /// <param name="natureId">Current nature id</param>
        /// <returns>Result of the operation</returns>
        Task<bool> IsNatureExists(int natureId);
    }
}
