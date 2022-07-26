using FH.EventProcessing;
using FH.EventProcessing.Config;
using System.Net;

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
    // Allow any CORS in development
    app.UseCors(options =>
    {
        options
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

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
    services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("FAFixturesInMem"));
    services.AddScoped<IRepository, FixturesRepository>();

    services.AddHttpClient<IDataLoader, DataLoader>().ConfigurePrimaryHttpMessageHandler(config => new HttpClientHandler
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
    });

    services.AddHostedService<MessageBusSubscriber>();
    services.AddSingleton<IMessageBusPublisher, BaseMessageBusPublisher>();
    services.AddSingleton<IEventProcessor, EventProcessor>();

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}

/// <summary>
/// Sets up the configs within the application
/// </summary>
void SetupConfigs(WebApplicationBuilder builder)
{
    builder.Services
        .Configure<EndpointOptions>(builder.Configuration.GetSection("Endpoints"))
        .Configure<RabbitMQOptions>(builder.Configuration.GetSection("RabbitMQ"));
}
