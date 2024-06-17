using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Expense.CommandHandlers
{
    public static class ServicesCollectionExtension
    {
        /// <summary>
        /// Injection of the mediator command handlers to the main application.
        /// </summary>
        /// <param name="services">Services collection</param>
        public static void AddMediatRCommandHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
