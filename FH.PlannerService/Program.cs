var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SetupConfigs();
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

app.SetupQueueAndTriggerDataLoad(new()
{
    new()
    {
        Event = EventType.DataLoadRequest.ConvertEventTypeToEventString(),
        Source = EventSource.FantasyAllsvenskan,
        Data = new(EventType.TeamsPublished, Assembly.GetEntryAssembly().GetName().Name)
    },
    new()
    {
        Event = EventType.DataLoadRequest.ConvertEventTypeToEventString(),
        Source = EventSource.FantasyAllsvenskan,
        Data = new(EventType.PlayersPublished, Assembly.GetEntryAssembly().GetName().Name)
    },
    new()
    {
        Event = EventType.DataLoadRequest.ConvertEventTypeToEventString(),
        Source = EventSource.FantasyAllsvenskan,
        Data = new(EventType.FixturesPublished, Assembly.GetEntryAssembly().GetName().Name)
    },
    new()
    {
        Event = EventType.DataLoadRequest.ConvertEventTypeToEventString(),
        Source = EventSource.FPL,
        Data = new(EventType.TeamsPublished, Assembly.GetEntryAssembly().GetName().Name)
    },
    new()
    {
        Event = EventType.DataLoadRequest.ConvertEventTypeToEventString(),
        Source = EventSource.FPL,
        Data = new(EventType.PlayersPublished, Assembly.GetEntryAssembly().GetName().Name)
    },
    new()
    {
        Event = EventType.DataLoadRequest.ConvertEventTypeToEventString(),
        Source = EventSource.FPL,
        Data = new(EventType.FixturesPublished, Assembly.GetEntryAssembly().GetName().Name)
    },
});

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("PlannerInMem"));
    services.AddScoped<IRepository, PlannerDataRepository>();

    services.AddSingleton<IMessageBusPublisher, BaseMessageBusPublisher>();
    services.AddHostedService<MessageBusSubscriber>();
    services.AddSingleton<IEventProcessor, EventProcessor>();

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}

void SetupConfigs()
{
    builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection("RabbitMQ"));
}
