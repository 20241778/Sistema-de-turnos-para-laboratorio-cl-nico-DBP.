using LabClinic.Infrastructure.Extensions;
using LabClinic.Application.Services;
using LabClinic.Applicattion.Interfaces;
using LabClinic.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar cadena de conexión (puedes moverla a appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=(localdb)\\mssqllocaldb;Database=LabClinicDB;Trusted_Connection=true;MultipleActiveResultSets=true";

// Agregar Infrastructure (DbContext, Repositories, UnitOfWork)
builder.Services.AddLabClinicInfrastructure(connectionString);

// Agregar AutoMapper
builder.Services.AddAutoMapper(typeof(LabClinic.Application.Mappings.MappingProfile));

// Registrar servicios de aplicación
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<ITecnicoService, TecnicoService>();
builder.Services.AddScoped<IPruebaService, PruebaService>();
builder.Services.AddScoped<ICitaService, CitaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
