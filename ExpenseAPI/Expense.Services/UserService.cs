
namespace Expense.Services
{
    using Expense.Repositories.Interfaces;
    using Expense.Services.Interfaces;

    /// <summary>
    /// User service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor of <see cref="UserService"/>.
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Verify if the user exists in database.
        /// </summary>
        /// <param name="userId">Current user id</param>
        /// <returns>Result of the operation</returns>
        public async Task<bool> IsUserExists(int userId)
        {
            return await _userRepository.IsUserExists(userId);
        }
    }
}
