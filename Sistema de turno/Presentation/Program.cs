using Presentation.Services.Implementations;
using Presentation.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7247/");
});

// Register typed clients
builder.Services.AddHttpClient<CitaApiService>(client => {
    client.BaseAddress = new Uri("https://localhost:7241/");
});
builder.Services.AddHttpClient<PacienteApiService>(client => {
    client.BaseAddress = new Uri("https://localhost:7241/");
});
builder.Services.AddHttpClient<TecnicoApiService>(client => {
    client.BaseAddress = new Uri("https://localhost:7241/");
});
builder.Services.AddHttpClient<PruebaApiService>(client => {
    client.BaseAddress = new Uri("https://localhost:7241/");
});

// register services
builder.Services.AddScoped<Presentation.Services.Interfaces.ICitaService, Presentation.Services.Implementations.CitaApiService>();
builder.Services.AddScoped<Presentation.Services.Interfaces.IPacienteService, Presentation.Services.Implementations.PacienteApiService>();
builder.Services.AddScoped<Presentation.Services.Interfaces.ITecnicoService, Presentation.Services.Implementations.TecnicoApiService>();
builder.Services.AddScoped<Presentation.Services.Interfaces.IPruebaService, Presentation.Services.Implementations.PruebaApiService>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
