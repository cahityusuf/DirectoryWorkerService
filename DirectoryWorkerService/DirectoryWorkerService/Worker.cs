using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Data;
using Abstractions.Dtos;
using Abstractions.Options;
using Abstractions.Results;
using DirectoryWorkerService.Services;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DirectoryWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMqService _rabbitMqService;
        private IModel _channel;
        private readonly ReportService _reportService;
        private readonly IServiceProvider _serviceProvider;
        public Worker(ILogger<Worker> logger, RabbitMqService rabbitMqService, IServiceProvider serviceProvider, ReportService reportService)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
            _serviceProvider = serviceProvider;
            _reportService = reportService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMqService.Connect();
            _channel.BasicQos(0, 1, false);
            _logger.LogInformation("basladi");
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += Consumer_Received;

            _channel.BasicConsume(RabbitMqService.QueueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var reportMessage = JsonSerializer.Deserialize<CreateReportMessageDto>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            using (var httpClient = new HttpClient())
            {
                var res = _reportService.Rapor();

                if (res.Success)
                {
                    foreach (var item in res.Data)
                    {
                        item.ReportsId = reportMessage.ReportId;
                    }
                    ReportsDto report = new()
                    {
                        ReportDetail = res.Data,
                        Id = reportMessage.ReportId
                    };
                    var stringPayload = JsonSerializer.Serialize(report);

                    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    var response =
                        await httpClient.PostAsync(
                            $"{RabbitMqOptions.BaseUlr}Report/ReportCapture?id={reportMessage.ReportId}", httpContent);


                    _logger.LogInformation($"kuyruk okundu {reportMessage.ReportId}");
                    _channel.BasicAck(@event.DeliveryTag, false);

                }

            }
        }

    }
}
