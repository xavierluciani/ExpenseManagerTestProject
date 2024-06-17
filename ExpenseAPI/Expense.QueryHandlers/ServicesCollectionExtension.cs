using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Expense.QueryHandlers
{
    public static class ServicesCollectionExtension
    {
        /// <summary>
        /// Injection of the mediator query handlers to the main application.
        /// </summary>
        /// <param name="services">Services collection</param>
        public static void AddMediatRQueryHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
