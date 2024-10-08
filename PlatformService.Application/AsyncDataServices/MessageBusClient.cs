using Microsoft.Extensions.Configuration;
using PlatformService.Contracts.Application;
using PlatformService.Contracts.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlatformService.Application.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private IConnection _connection;
        private IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQHost"],
                Port = int.Parse(configuration["RabbitMQPort"])
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare("trigger", ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQConnectionShutdown;
                Console.WriteLine("--> Estamos conectados a rabbitmq");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"-----------> No he podido conectar a Message Bus! {ex}");
            }
        }

        public void PublishNewPlatform(PlatformPublished platformPublished)
        {
            var platformSerialized = JsonSerializer.Serialize(platformPublished);
            if (_connection.IsOpen)
            {
                Console.WriteLine($"--> RabbitMQ conenction open sending message");
                SendMessage(platformSerialized);
            }
            else
            {
                Console.WriteLine($"--> Algo va mal con RabbitMQ ");
            }
        }

        public void Dispose()
        {
            Console.WriteLine("MessageBus disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish("trigger", string.Empty, null, body);
            Console.WriteLine($"--> Mensaje enviado! {message}");
        }

        private void RabbitMQConnectionShutdown(object sender, EventArgs e)
        {
            Console.WriteLine("--> Rabbitmq connection timeout!!!!!");
        }
    }
}
