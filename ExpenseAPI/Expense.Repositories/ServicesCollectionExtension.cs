using Expense.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Expense.Repositories
{
    public static class ServicesCollectionExtension
    {
        /// <summary>
        /// Inject repositories to the main application.
        /// </summary>
        /// <param name="services">Services collection</param>
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INatureRepository, NatureRepository>();
        }
    }
}
