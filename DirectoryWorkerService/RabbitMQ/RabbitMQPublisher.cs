using System.Text;
using System.Text.Json;
using Abstractions.Dtos;
using RabbitMQ;

namespace DirectoryWorkerService
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMqService _rabbitMqService;

        public RabbitMQPublisher(RabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        public void Publish(CreateReportMessageDto createReportMessage)
        {
            var channel = _rabbitMqService.Connect();

            var bodyString = JsonSerializer.Serialize(createReportMessage);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);
            var properties = channel.CreateBasicProperties();

            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMqService.ExchangeName,routingKey:RabbitMqService.RoutingReport,true,basicProperties:properties,body:bodyByte);
        }
    }
}
