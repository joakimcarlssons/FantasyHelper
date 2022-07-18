namespace FH.FA.FantasyDataProvider.MessagePublishers
{
    public class MessageBusPublisher : IMessageBusPublisher
    {
        #region Private Members

        private const string exchangeName = "trigger";

        private readonly RabbitMQOptions _rabbitMQConfig;
        private IConnection _connection;
        private IModel _channel;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageBusPublisher(IOptions<RabbitMQOptions> rabbitMQConfig)
        {
            _rabbitMQConfig = rabbitMQConfig.Value;

            InitializeRabbitMQ();
        }

        #endregion

        public void PublishTeams(TeamsPublishedDto teamsPublishedDto)
        {
            var message = JsonSerializer.Serialize(teamsPublishedDto);

            if (_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ connection is open, sending message...");
                SendMessage(message, exchangeName, "");
            }
            else
            {
                // If connection is closed
                Console.WriteLine("--> RabbitMQ connection is closed, could not send message...");
            }
        }

        public void SendMessage(string message, string exchange, string routingKey)
        {
            // Encode message
            var body = Encoding.UTF8.GetBytes(message);

            // Publish message
            _channel.BasicPublish(
                exchange: exchange,
                routingKey: routingKey,
                basicProperties: null,
                body: body);

            Console.WriteLine($"--> Message sent: { message }");
        }

        #region Private Members

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQConfig.Host,
                Port = int.Parse(_rabbitMQConfig.Port)
            };

            try
            {
                // Setup connection
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                // Setup exchange and exchange type
                _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

                // Subscribe to shutdown event
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                // Log successful connection
                Console.WriteLine("--> Connected to message bus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not connect to the Message Bus: { ex.Message }");
                throw;
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"--> RabbitMQ connection shutdown");
        }

        #endregion
    }
}
