using Core.Repositories;
using Core.Services;
using DataAccess;
using DataAccess.Repositories;
using DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using WebShop.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Get the connection string from the app settings.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register ApplicationDbContext in the DI container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication must be placed before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
