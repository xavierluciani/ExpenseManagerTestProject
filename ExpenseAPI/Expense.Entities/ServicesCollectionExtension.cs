using Expense.Common.Config;
using Expense.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Expense.Entities
{
    public static class ServicesCollectionExtension
    {
        /// <summary>
        /// Inject database context to the main application.
        /// </summary>
        /// <param name="services">Services collection</param>
        /// <param name="config">Database configuration</param>
        public static void AddDatabaseContext(this IServiceCollection services, DatabaseConfig config)
        {
            services.AddDbContext<DBBContext>(o => o.UseSqlServer(config.ConnectionString), optionsLifetime: ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Initialization of the database if tables are empty.
        /// </summary>
        /// <param name="services">Services collection</param>
        /// <param name="dbContext">Database context</param>
        public static void InitDatabase(this IServiceCollection services, DBBContext dbContext)
        {
            if (!dbContext.Currencies.Any())
            {
                dbContext.Currencies.AddRange(new List<Currency>()
                {
                    new Currency() { CurCode = "DOL", CurName = "DOLLAR" },
                    new Currency() { CurCode = "ROU", CurName = "ROUBLE" },
                });
                dbContext.SaveChanges();
            }

            if (!dbContext.Natures.Any())
            {
                dbContext.Natures.AddRange(new List<Nature>()
                {
                    new Nature() { NatCode = "REST", NatName = "Restaurant" },
                    new Nature() { NatCode = "HOT", NatName = "Hotel" },
                    new Nature() { NatCode = "MISC", NatName = "Misc" },
                });
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any() && dbContext.Currencies.Any())
            {
                var currencies = dbContext.Currencies.ToList();

                dbContext.Users.AddRange(new List<User>()
                {
                    new User { Surname = "Anthony", Name = "Stark", IdCur = currencies.FirstOrDefault()!.IdCur },
                    new User { Surname = "Natasha", Name = "Romanova", IdCur = currencies.LastOrDefault()!.IdCur },
                });
                dbContext.SaveChanges();
            }
        }
    }
}
