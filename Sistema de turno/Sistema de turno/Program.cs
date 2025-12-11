/*using LabClinic.Infrastructure.Extensions;
using LabClinic.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();



//// Learn more about configuring Swagger/OpenAPI at  https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar cadena de conexión (puedes moverla a appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=(localdb)\\mssqllocaldb;Database=SistemaTurnos;Trusted_Connection=true;MultipleActiveResultSets=true";

// Agregar Infrastructure (DbContext, Repositories, UnitOfWork)
builder.Services.AddLabClinicInfrastructure(connectionString);

// Registrar servicios de aplicación y AutoMapper desde el proyecto Application
builder.Services.AddLabClinicApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Attribute routing
app.MapControllers();

// Conventional fallback route (optional)
app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Home}/{action=Index}/{id?}");

app.Run();*/

using LabClinic.Infrastructure.Extensions;
using LabClinic.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// ✅ AGREGAR CORS AQUÍ - DESPUÉS DE AddControllers()
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar cadena de conexión (puedes moverla a appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=SistemaTurnos;Trusted_Connection=true;MultipleActiveResultSets=true";

// Agregar Infrastructure (DbContext, Repositories, UnitOfWork)
builder.Services.AddLabClinicInfrastructure(connectionString);

// Registrar servicios de aplicación y AutoMapper desde el proyecto Application
builder.Services.AddLabClinicApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ USAR CORS AQUÍ - ANTES DE UseAuthorization()
app.UseCors("AllowFrontend");

app.UseAuthorization();

// Attribute routing
app.MapControllers();

// Conventional fallback route (optional)
app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Home}/{action=Index}/{id?}");

app.Run();