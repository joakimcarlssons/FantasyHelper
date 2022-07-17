var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SetupConfigs(builder);
ConfigureServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

/// <summary>
/// Handles the configuration of the services within the application
/// </summary>
void ConfigureServices(IServiceCollection services)
{
    // Add database for db context
    services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("FPLInMem"));
    services.AddScoped<IDataProviderRepository, FPLDataProviderRepository>();

    services.AddHttpClient<IDataLoader, DataLoader>();
    services.AddHostedService<PeriodicDataLoader>();

    // Add auto mapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}

/// <summary>
/// Sets up the configs within the application
/// </summary>
void SetupConfigs(WebApplicationBuilder builder)
{
    builder.Services.Configure<FPLOptions>(builder.Configuration.GetSection("FPL"));
}
