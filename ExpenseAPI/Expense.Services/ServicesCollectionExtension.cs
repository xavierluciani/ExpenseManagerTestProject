
namespace Expense.Services
{
    using Expense.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class ServicesCollectionExtension
    {
        /// <summary>
        /// Inject services to the main application.
        /// </summary>
        /// <param name="services">Services collection</param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INatureService, NatureService>();
        }

        /// <summary>
        /// Inject automapper to the main application.
        /// </summary>
        /// <param name="services">Services collection</param>
        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
