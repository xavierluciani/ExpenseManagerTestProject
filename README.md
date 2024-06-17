# Expense manager

Solution is developed in .NET 8 version implementing CQRS pattern architecture.
Solution allows user to manage expenses.

### Creating an expense

This API (REST) must make it possible to:
 - Create an expense taking into account the validation rules.

Rules for validating an expense:
- An expense cannot have a date in the future,
- An expense cannot be dated more than 3 months ago,
- The comment is obligatory,
- A user cannot declare the same expense twice (same date and same amount),
- The currency of the expense must be identical to that of the user.

### List of expenses
This API (REST) must make it possible to:

- List expenses for a given user,
- Sort expenses by amount or date,
- Display all the properties of the expense; the user of the expense must appear in the form {FirstName} {LastName} (eg: "Anthony Stark").

## Main technical stack
.NET 8 - SQL Server 2022 - EF Core 8 - Automapper - Mediatr - Moq - UnitTesting
Entities generated with EF Core Power Tools

### Database initialization
Data base must be created with script.sql located in DBB folder.
First datas are stored at the startup of the solution if tables are empty.

### Architecture
- API: Controllers with endpoint to be called, dependency injection, database at first init if needed.
- CommandQuery: Command and Queries to be called by endpoint methods.
- Handlers: Actions to be performed when command & handlers are called.
- Services: Retrieve functional data with repositories & convert entities into DTO.
- Repositories: Perform queries to database and return results as entities.
- Common: All common elements & classes used in the whole solution.
- Tests: Test project to check normal behavior and all failures behaviors of endpoints.