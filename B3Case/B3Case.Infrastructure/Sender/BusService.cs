using B3Case.Application.Services.RabbitServices.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace B3Case.Application.Services.RabbitServices
{
    public class BusService : IBusService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BusService> _logger;

        public BusService(IConfiguration configuration, ILogger<BusService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
            };
        }

        public void SendMessage(string message, string queue)
        {
            try
            {
                _logger.LogInformation("Attempting to send message to queue: {Queue} at {time}", queue, DateTimeOffset.Now);

                using var connection = _connectionFactory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queue,
                                     basicProperties: null,
                                     body: body);

                _logger.LogInformation("Message sent to queue: {Queue} successfully at {time}", queue, DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send message to queue: {Queue} at {time}", queue, DateTimeOffset.Now);
            }
        }

        public T Consuming<T>(string queue)
        {
            T result = default;

            try
            {
                _logger.LogInformation("Starting to consume message from queue: {Queue} at {time}", queue, DateTimeOffset.Now);

                using var connection = _connectionFactory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        result = JsonSerializer.Deserialize<T>(message);

                        _logger.LogInformation("Message consumed from queue: {Queue} with content: {Message} at {time}", queue, message, DateTimeOffset.Now);

                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to process message from queue: {Queue} at {time}", queue, DateTimeOffset.Now);
                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }
                };

                channel.BasicConsume(queue: queue,
                                     autoAck: false,
                                     consumer: consumer);

                while (result == null) { Thread.Sleep(100); }

                _logger.LogInformation("Finished consuming message from queue: {Queue} at {time}", queue, DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while consuming message from queue: {Queue} at {time}", queue, DateTimeOffset.Now);
            }

            return result;
        }

    }
}