using InventorySystem.Infrastructure;
using InventorySystem.Application;
using InventorySystem.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Get the connection string from settings
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Check if we are running in Azure (Azure always has a variable called WEBSITE_SITE_NAME)
var inAzure = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME"));

if (inAzure)
{
    // ☁️ CLOUD MODE: Use SQL Server
    builder.Services.AddDbContext<InventoryDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    // 💻 LAPTOP MODE: Use SQLite
    builder.Services.AddDbContext<InventoryDbContext>(options =>
        options.UseSqlite(connectionString));
}

// 1.1 Tell the app to use our 'ApplicationUser' for security
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<InventoryDbContext>()
    .AddDefaultTokenProviders();

// 1.2 Get the Key from appsettings.json
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

// 1.3 Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// 2. Add Services
builder.Services.AddHttpContextAccessor(); // ?? NEW: Enables reading the current user from token
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger to accept Bearer Tokens
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI...\""
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()   // Allows localhost:4200
                        .AllowAnyMethod()   // Allows GET, POST, PUT, etc.
                        .AllowAnyHeader()); // Allows all headers
});


var app = builder.Build();

// Auto-apply migrations in Azure
if (inAzure)
{
    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
            db.Database.Migrate(); // Apply pending migrations automatically
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw; // Re-throw to see the actual error in Azure logs
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAll");

// Enable Authentication & Authorization (ORDER MATTERS!)
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
