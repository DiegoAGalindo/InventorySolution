using InventoryAPIRead.Data;
using InventoryAPIRead.Repositories;
using InventoryAuthLibrary;
using InventorySharedLibrary.Interfaces;
using InventorySharedLibrary.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<IProductRepository, ProductReadRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryReadRepository>();
builder.Services.AddScoped<IInventoryMovementRepository, InventoryMovementReadRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();