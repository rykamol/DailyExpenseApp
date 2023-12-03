
using ExpenseManagement.Data;
using ExpenseManagement.Data.Repository;
using ExpenseManagement.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//var dbHost = "localhost,8002"; //Environment.GetEnvironmentVariable("DB_HOST");
//var dbName = "ExpenseDB"; //Environment.GetEnvironmentVariable("DB_NAME");
//var dbPassword = "Kamol9943";//Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
//var connectionString = $"Data Source={dbHost};Database={dbName};User ID=SA; Password={dbPassword};";
//bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

//if (isDevelopment)
//{
builder.Services.AddDbContext<ExpenseDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//}
//else
//{
//builder.Services.AddDbContext<ExpenseDbContext>(options => options.UseSqlServer(connectionString));
//}

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Expenses}/{action=Index}/{id?}");

//app.MapRazorPages();

app.Run();
