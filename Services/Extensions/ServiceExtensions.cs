namespace spendo_be.Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IBudgetService, BudgetService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<ITransferService, TransferService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}