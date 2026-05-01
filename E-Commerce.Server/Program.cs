using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.Tokens;
using E_Commerce.Application.Services;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Infrastructure.Repositories;
using E_Commerce.Infrastructure.Services;
using E_Commerce.Server.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//-------------------------------------------CORS------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorOrigin",
        policy => policy.WithOrigins("https://localhost:7039")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
//-------------------------------------------CORS------------------------------------------------------

//------------------------------------------E.FCORE----------------------------------------------------
builder.Services.AddDbContext<ECommerceDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDBContext")));
//------------------------------------------E.FCORE----------------------------------------------------

//------------------------------------------IDENTITYCORE-----------------------------------------------
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options => // Cambia AddIdentityCore por AddIdentity
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<ECommerceDBContext>()
    .AddDefaultTokenProviders();
//------------------------------------------IDENTITYCORE-----------------------------------------------

//-------------------------------------------Serilog + Seq --------------------------------------------
builder.Host.UseSerilog((context, logConfg) => logConfg.ReadFrom.Configuration(context.Configuration));
//-------------------------------------------Serilog + Seq ---------------------------------------------

// ------------------------------------- JSON WEB TOKENS------------------------------------------------
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
// ------------------------------------- JSON WEB TOKENS------------------------------------------------

// ------------------------------------- FluentValidator ------------------------------------------------
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
// ------------------------------------- FluentValidator ------------------------------------------------

// ------------------------------------- SERVICIOS ------------------------------------------------
// Los servicios que tengan un comentario al costado, es para diferenciar y reconocer a que esta ligado y configurado.
builder.Services.AddProblemDetails(); //MIDDLEWARE
builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); //JWT
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>(); //JWT
builder.Services.AddScoped<IGenerarTokenService, GenerarTokenService>();//JWT
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddHttpContextAccessor(); //JWT
builder.Services.AddAuthorization(); //JWT
builder.Services.AddControllers();
builder.Services.AddOpenApi();
// ------------------------------------- SERVICIOS ------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

app.UseExceptionHandler();

app.UseHttpsRedirection();

// --- JWT ---
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
