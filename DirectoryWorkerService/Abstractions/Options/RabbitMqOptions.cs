using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Options
{
    public class RabbitMqOptions
    {

        public const string ExchangeName = "RabbitMqSettings:ExchangeName";
        public const string RoutingReport = "RabbitMqSettings:RoutingReport";
        public const string QueueName = "RabbitMqSettings:QueueName";
        public const string BaseUlr = "https://localhost:44360/v1/";
    }
}
