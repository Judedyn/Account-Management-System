using CustomerApi.Data;
using CustomerApi.Models;
using CustomerApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlite("Data Source=accounts.db"));

builder.Services.AddScoped<AccountService>();

// OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Register a new account
app.MapPost("/accounts", async (Account account, AccountService service) =>
{
    var createdAccount = await service.RegisterAsync(account);

    if (createdAccount == null)
    {
        return Results.BadRequest("An account with this email already exists.");
    }

    return Results.Created($"/accounts/{createdAccount.Id}", createdAccount);
})
.WithName("RegisterAccount")
.WithSummary("Register a new customer account")
.WithDescription("Creates a new account with first name, last name, and email.")
.Produces<Account>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest);

// Get all accounts
app.MapGet("/accounts", async (AccountService service) =>
{
    var accounts = await service.GetAllAsync();
    return Results.Ok(accounts);
})
.WithName("GetAllAccounts")
.WithSummary("Get all customer accounts")
.Produces<List<Account>>(StatusCodes.Status200OK);

// Get account by ID
app.MapGet("/accounts/{id:int}", async (int id, AccountService service) =>
{
    var account = await service.GetByIdAsync(id);

    return account is null
        ? Results.NotFound("Account not found.")
        : Results.Ok(account);
})
.WithName("GetAccountById")
.WithSummary("Get account by ID")
.Produces<Account>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

// Get account by email
app.MapGet("/accounts/by-email/{email}", async (string email, AccountService service) =>
{
    var account = await service.GetByEmailAsync(email);

    return account is null
        ? Results.NotFound("Account not found.")
        : Results.Ok(account);
})
.WithName("GetAccountByEmail")
.WithSummary("Get account by email")
.Produces<Account>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

// Update account
app.MapPut("/accounts/{id:int}", async (int id, Account updatedAccount, AccountService service) =>
{
    try
    {
        var account = await service.UpdateAsync(id, updatedAccount);

        return account is null
            ? Results.NotFound("Account not found.")
            : Results.Ok(account);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithName("UpdateAccount")
.WithSummary("Update customer account")
.Produces<Account>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status404NotFound);

app.MapGet("/", () => "Account Management API is running. Go to /swagger to test.");

app.Run();

await app.RunAsync();