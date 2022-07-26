namespace FH.FA.FantasyDataProvider.EventProcessing
{
    public class MessageBusSubscriber : BackgroundService
    {
        #region Private Members

        private const string exchangeName = "trigger";
        private string _queueName = Assembly.GetEntryAssembly().GetName().Name;

        private IConnection _connection;
        private IModel _channel;

        private readonly RabbitMQOptions _rabbitMQConfig;
        private readonly IEventProcessor _eventProcessor;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageBusSubscriber(IOptions<RabbitMQOptions> rabbitMQConfig, IEventProcessor eventProcessor)
        {
            _rabbitMQConfig = rabbitMQConfig.Value;
            _eventProcessor = eventProcessor;

            InitializeRabbitMQ();
        }

        #endregion

        #region Overrides

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Throw execeptionif cancellation is requested
            stoppingToken.ThrowIfCancellationRequested();

            // Add consumer and set action for when event is received
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine("--> Event Received!");

                // Get event body/message
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                // Process event
                _eventProcessor.ProcessEvent(message);
            };

            // Start consume again
            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            // Close down RabbitMQ subscription if it's open
            if (_channel.IsOpen)
            {
                _connection.Close();
                _channel.Close();
            }

            base.Dispose();
        }

        #endregion

        #region Private Methods

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQConfig.Host,
                Port = int.Parse(_rabbitMQConfig.Port)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
            _channel.QueueBind(queue: _queueName, exchange: exchangeName, routingKey: _queueName);

            Console.WriteLine("--> Listening on Message bus...");

            // Subscribe to shutdown event
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection shut down...");
        }

        #endregion
    }
}
