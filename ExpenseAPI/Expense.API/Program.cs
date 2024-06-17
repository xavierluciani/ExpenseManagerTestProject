using Expense.Common.Config;
using Expense.CommandHandlers;
using Expense.QueryHandlers;
using Expense.Entities;
using Expense.Repositories;
using Expense.Services;
using Expense.Common.Exception;
using Expense.Entities.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(p =>
{
    var loggerFactory = p.GetRequiredService<ILoggerFactory>();
    return loggerFactory.CreateLogger("Logger");
});

var settings = builder.Configuration.GetSection("DatabaseConfig").Get<DatabaseConfig>();

if (settings == null)
{
    throw new ConfigurationException("DatabaseConfig is not filled in configuration appsettings");
}

// Inject context, repositories, services
builder.Services.AddDatabaseContext(settings);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMapping();

// Inject CQRS resources
builder.Services.AddMediatRCommandHandlers();
builder.Services.AddMediatRQueryHandlers();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()        ;
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

var factory = app.Services.GetService<IServiceScopeFactory>();
if (factory == null)
{
    throw new ConfigurationException($"Unable to get {nameof(IServiceScopeFactory)} service");
}

// Init database if no data in tables
using (var serviceScope = factory.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<DBBContext>();
    builder.Services.InitDatabase(dbContext);
}

app.MapControllers();

app.Run();