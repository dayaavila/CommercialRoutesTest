using CommercialRoutes.Application.Interfaces;
using CommercialRoutes.Application.Services;
using CommercialRoutes.Domain.Interfaces;
using CommercialRoutes.Domain.Services;
using CommercialRoutes.Infrastructure.Interfaces;
using CommercialRoutes.Infrastructure.Options;
using CommercialRoutes.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

// Application Services
builder.Services.AddScoped<IRoutesAppService, RoutesAppService>();

// Domain Services
builder.Services.AddScoped<IRoutesService, RoutesService>();
builder.Services.AddScoped<IDistancesService, DistanceService>();
builder.Services.AddScoped<IPlanetsService, PlanetsService>();
builder.Services.AddScoped<IPricesService, PricesService>();
builder.Services.AddScoped<IRebelsService, RebelsService>();

// Infrastructure Services
builder.Services.AddScoped<IDistancesApiService, DistanceApiService>();
builder.Services.AddScoped<IPlanetsApiService, PlanetsApiService>();
builder.Services.AddScoped<IPricesApiService, PricesApiService>();
builder.Services.AddScoped<IRebelsApiService, RebelsApiService>();

// Options
builder.Services.Configure<UrlEndpoints>(builder.Configuration.GetSection("UrlEndpoints"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
