
namespace Expense.Repositories.Interfaces
{
    /// <summary>
    /// User repository interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Verify if the user exists in database.
        /// </summary>
        /// <param name="userId">Current user id</param>
        /// <returns>Result of the operation</returns>
        Task<bool> IsUserExists(int userId);
    }
}
