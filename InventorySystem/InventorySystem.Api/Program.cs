using InventorySystem.Infrastructure;
using InventorySystem.Application;
using InventorySystem.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlite(connectionString));

// 2. Add Services
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()   // Allows localhost:4200
                        .AllowAnyMethod()   // Allows GET, POST, PUT, etc.
                        .AllowAnyHeader()); // Allows all headers
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("AllowAll");
// ---------------------------------------------------------

app.UseAuthorization();
app.MapControllers();

app.Run();
