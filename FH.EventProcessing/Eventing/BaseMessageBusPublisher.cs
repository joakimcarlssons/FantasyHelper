using FH.EventProcessing.Config;
using FH.EventProcessing.Dtos;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FH.EventProcessing
{
    public class BaseMessageBusPublisher : IMessageBusPublisher
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
        public BaseMessageBusPublisher(IOptions<RabbitMQOptions> rabbitMQConfig)
        {
            _rabbitMQConfig = rabbitMQConfig.Value;

            InitializeRabbitMQ();
        }

        #endregion

        public void PublishData<T>(DataPublishedDto<T> data, string routingKey = "", string queueName = null)
        {
            var message = JsonSerializer.Serialize(data);

            if (_connection.IsOpen)
            {
                Console.WriteLine($"--> RabbitMQ connection is open, triggering event { data.Event }...");
                SendMessage(message, exchangeName, routingKey, queueName);
            }
            else
            {
                // If connection is closed
                Console.WriteLine($"--> RabbitMQ connection is closed, could not trigger event { data.Event }...");
            }
        }

        public void SendMessage(string message, string exchange, string routingKey, string queueName)
        {
            // Encode message
            var body = Encoding.UTF8.GetBytes(message);

            // Verify that queue exists if a queue is given
            if (queueName != null)
            {
                _channel.QueueDeclare(queueName, true, false, false, null);
                _channel.QueueBind(queue: queueName, exchange: exchange, routingKey: routingKey);
            }

            // Publish message
            _channel.BasicPublish(
                exchange: exchange,
                routingKey: routingKey,
                basicProperties: null,
                body: body);

            Console.WriteLine($"--> Message sent!");
        }

        public void SetupQueue(string queueName, bool durable, bool exclusive, bool autoDelete, IDictionary<string, object> arguments, string exchange, string routingKey)
        {
            _channel.QueueDeclare(
                queue: queueName,
                durable: durable, 
                exclusive: exclusive, 
                autoDelete: autoDelete, 
                arguments: arguments);
            _channel.QueueBind(
                queue: queueName,
                exchange: exchange, 
                routingKey: routingKey,
                arguments: arguments);
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
