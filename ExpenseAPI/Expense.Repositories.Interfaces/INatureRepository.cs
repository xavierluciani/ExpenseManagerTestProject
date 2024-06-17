
namespace Expense.Repositories.Interfaces
{
    /// <summary>
    /// Nature repository interface
    /// </summary>
    public interface INatureRepository
    {
        /// <summary>
        /// Verify if the nature exists in database.
        /// </summary>
        /// <param name="natureId">Current nature id</param>
        /// <returns>Result of the operation</returns>
        Task<bool> IsNatureExists(int natureId);
    }
}