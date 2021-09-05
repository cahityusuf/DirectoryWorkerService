using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Configuration;
using DirectoryWorkerService.Services;
using Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace DirectoryWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration Configuration = hostContext.Configuration;
                    services.AddSingleton(s => new ConnectionFactory()
                    {
                        Uri = new Uri
                            (Configuration.GetSection("RabbitMqSettings:ConnectionString").Value),
                        DispatchConsumersAsync = true
                    });

                    services.AddSingleton<RabbitMqService>();
                    services.AddSingleton<RabbitMQPublisher>();

                    services.AddDbContext<DirectoryDbContext>(opts =>
                    {
                        opts.UseNpgsql(Configuration.GetConnectionString("DirectoryDbContext"));
                    });
                    //services.AddUnitOfWork<DirectoryDbContext>(dbContextoptionsAction);
                    services.AddSingleton<ReportService>();

                    services.AddHostedService<Worker>();
                });
    }
}
