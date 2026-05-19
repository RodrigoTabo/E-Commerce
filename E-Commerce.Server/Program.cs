using E_Commerce.Application.Interfaces;
using E_Commerce.Application.Interfaces.Atributos;
using E_Commerce.Application.Interfaces.AtributoValores;
using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.CarritoItems;
using E_Commerce.Application.Interfaces.Carritos;
using E_Commerce.Application.Interfaces.Ciudades;
using E_Commerce.Application.Interfaces.Domicilios;
using E_Commerce.Application.Interfaces.Favoritos;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.Marcas;
using E_Commerce.Application.Interfaces.MetodoEnvios;
using E_Commerce.Application.Interfaces.MetodoPagos;
using E_Commerce.Application.Interfaces.Modelos;
using E_Commerce.Application.Interfaces.Ordenes;
using E_Commerce.Application.Interfaces.OrdenItems;
using E_Commerce.Application.Interfaces.Pagos;
using E_Commerce.Application.Interfaces.Productos;
using E_Commerce.Application.Interfaces.ProductoVariantes;
using E_Commerce.Application.Interfaces.Reviews;
using E_Commerce.Application.Interfaces.TipoProductos;
using E_Commerce.Application.Interfaces.Tokens;
using E_Commerce.Application.Interfaces.User;
using E_Commerce.Application.Services;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Infrastructure.Repositories;
using E_Commerce.Infrastructure.Services;
using E_Commerce.Server.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//-------------------------------------------CORS------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorOrigin",
        policy => policy.WithOrigins("https://localhost:7039")
      .AllowAnyHeader()
      .AllowAnyMethod()
      .AllowCredentials());
});
//-------------------------------------------CORS------------------------------------------------------

//------------------------------------------E.FCORE----------------------------------------------------
builder.Services.AddDbContext<ECommerceDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDBContext")));
//------------------------------------------E.FCORE----------------------------------------------------

//------------------------------------------IDENTITYCORE-----------------------------------------------
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RoleClaimType = ClaimTypes.Role,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        ),

        ClockSkew = TimeSpan.Zero
    };
});
// ------------------------------------- JSON WEB TOKENS------------------------------------------------

// ------------------------------------- FluentValidator ------------------------------------------------
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
// ------------------------------------- FluentValidator ------------------------------------------------

// ------------------------------------- SERVICIOS ------------------------------------------------
// Los servicios que tengan un comentario al costado son para diferenciar y reconocer a que esta ligado y configurado.
builder.Services.AddProblemDetails(); //MIDDLEWARE
builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); //JWT
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>(); //JWT
builder.Services.AddScoped<IGenerarTokenService, GenerarTokenService>();//JWT
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IModeloRepository, ModeloRepository>();
builder.Services.AddScoped<IModeloService, ModeloService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IGenerarTokenService, GenerarTokenService>();
builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>(); // Inserts DB
builder.Services.AddScoped<ICarritoItemsService, CarritoItemsService>();
builder.Services.AddScoped<ICarritoItemsRepository, CarritoItemsRepository>();
builder.Services.AddScoped<ICarritoService, CarritoService>();
builder.Services.AddScoped<ICarritoRepository, CarritoRepository>();
builder.Services.AddScoped<IOrdenService, OrdenService>();
builder.Services.AddScoped<IOrdenRepository, OrdenRepository>();
builder.Services.AddScoped<IOrdenItemService, OrdenItemService>();
builder.Services.AddScoped<IMetodoEnvioService, MetodoEnvioService>();
builder.Services.AddScoped<IMetodoEnvioRepository, MetodoEnvioRepository>();
builder.Services.AddScoped<IDomicilioService, DomicilioService>();
builder.Services.AddScoped<IDomicilioRepository, DomicilioRepository>();
builder.Services.AddScoped<IMetodoPagoService, MetodoPagoService>();
builder.Services.AddScoped<IMetodoPagoRepository, MetodoPagoRepository>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IAtributoService, AtributoService>();
builder.Services.AddScoped<IAtributoRepository, AtributoRepository>();
builder.Services.AddScoped<IAtributoValorService, AtributoValorService>();
builder.Services.AddScoped<IAtributoValorRepository, AtributoValorRepository>();
builder.Services.AddScoped<IMarcaService, MarcaService>();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<ITipoProductoService, TipoProductoService>();
builder.Services.AddScoped<ITipoProductoRepository, TipoProductoRepository>();
builder.Services.AddScoped<IProductoVarianteService, ProductoVarianteService>();
builder.Services.AddScoped<IProductoVarianteRepository, ProductoVarianteRepository>();
builder.Services.AddScoped<IDomicilioService, DomicilioService>();
builder.Services.AddScoped<IDomicilioRepository, DomicilioRepository>();
builder.Services.AddScoped<ICiudadService, CiudadService>();
builder.Services.AddScoped<ICiudadRepository, CiudadRepository>();
builder.Services.AddScoped<IImagenStorageService, ImagenStorage>();
builder.Services.AddScoped<IPdfStorageService, PdfStorageService>();
builder.Services.AddScoped<IFavoritoRepository, FavoritoRepository>();
builder.Services.AddScoped<IFavoritoService, FavoritoService>();
builder.Services.AddScoped<IdentitySeedService>();
builder.Services.AddHttpContextAccessor(); //JWT
builder.Services.AddAuthorization(); //JWT
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddOpenApi();
// ------------------------------------- SERVICIOS ------------------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Habilita la generación del documento OpenAPI (ya incluido por defecto en .NET 10)
    app.MapOpenApi();

    // Habilita Swagger UI
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API v1");
    });
}


app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("AllowBlazorOrigin");

// --- JWT ---
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
try
{
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<ECommerceDBContext>();
    await db.Database.MigrateAsync();

    var seed = scope.ServiceProvider.GetRequiredService<IdentitySeedService>();
    await seed.SeedAsync();
}
catch (Exception ex)
{
    Console.WriteLine("DB/Seed error: " + ex.Message);
}

app.Run();
