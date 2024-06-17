
namespace Expense.Services.Interfaces
{
    /// <summary>
    /// User service interface
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Verify if the user exists in database.
        /// </summary>
        /// <param name="userId">Current user id</param>
        /// <returns>Result of the operation</returns>
        Task<bool> IsUserExists(int userId);
    }
}
