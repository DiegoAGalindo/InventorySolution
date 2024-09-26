using InventoryAPIWrite.Repositories;
using InventoryAuthLibrary;
using InventorySharedLibrary.Interfaces;
using InventorySharedLibrary.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<IProductRepository, ProductWriteRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryWriteRepository>();
builder.Services.AddScoped<IInventoryMovementRepository, InventoryMovementWriteRepository>();

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