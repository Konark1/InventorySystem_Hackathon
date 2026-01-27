using InventorySystem.Infrastructure;
using InventorySystem.Application;
using InventorySystem.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// TEMPORARY DEBUGGING: Hardcode the connection string to force it to work
var connectionString = "Server=tcp:inventory-server-konark.database.windows.net,1433;Initial Catalog=InventoryDB;Persist Security Info=False;User ID=sqladmin;Password=InventoryApp@2026;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

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
    // 💻 LAPTOP MODE: Use SQLite for local development
    builder.Services.AddDbContext<InventoryDbContext>(options =>
        options.UseSqlite("Data Source=inventory.db"));
}

// 1.1 Tell the app to use our 'ApplicationUser' for security
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<InventoryDbContext>()
    .AddDefaultTokenProviders();

// 1.2 Get the Key from appsettings.json or Azure configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];

// Defensive check: If JWT Key is missing, log error and use a fallback (for debugging)
if (string.IsNullOrEmpty(jwtKey))
{
    Console.WriteLine("WARNING: JWT Key not found in configuration! Using hardcoded key for debugging.");
    jwtKey = "ThisIsMySuperSecretKeyForMyInventoryApp123!";
}

var key = Encoding.ASCII.GetBytes(jwtKey);

// Get Issuer and Audience with fallbacks
var jwtIssuer = jwtSettings["Issuer"];
var jwtAudience = jwtSettings["Audience"];

if (string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
{
    Console.WriteLine("WARNING: JWT Issuer/Audience not found! Using Azure URL.");
    jwtIssuer = jwtIssuer ?? "https://inventory-api-konark.azurewebsites.net";
    jwtAudience = jwtAudience ?? "https://inventory-api-konark.azurewebsites.net";
}

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
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
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
    Console.WriteLine("Running in Azure - Attempting to apply migrations...");
    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
            Console.WriteLine("Database context created. Testing connection...");
            
            if (db.Database.CanConnect())
            {
                Console.WriteLine("Database connection successful! Applying migrations...");
                db.Database.Migrate();
                Console.WriteLine("Migrations applied successfully!");
            }
            else
            {
                Console.WriteLine("ERROR: Cannot connect to database!");
            }
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
            Console.WriteLine($"ERROR during migration: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            // Don't throw - let the app start so we can see errors in browser
        }
    }
}
else
{
    Console.WriteLine("Running locally - skipping Azure migrations.");
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
