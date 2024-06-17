
namespace Expense.Services
{
    using Expense.Repositories.Interfaces;
    using Expense.Services.Interfaces;

    public class NatureService : INatureService
    {
        private readonly INatureRepository _repository;

        /// <summary>
        /// Constructor of <see cref="NatureService"/>.
        /// </summary>
        /// <param name="repository">Nature repository</param>
        public NatureService(INatureRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Verify if the nature exists in database.
        /// </summary>
        /// <param name="natureId">Current nature id</param>
        /// <returns>Result of the operation</returns>
        public async Task<bool> IsNatureExists(int natureId)
        {
            return await this._repository.IsNatureExists(natureId);
        }
    }
}
