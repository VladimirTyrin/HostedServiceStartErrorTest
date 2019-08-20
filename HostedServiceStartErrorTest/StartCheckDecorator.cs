using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IApplicationLifetime = Microsoft.AspNetCore.Hosting.IApplicationLifetime;

namespace HostedServiceStartErrorTest
{
    public class StartCheckDecorator : IHostedService
    {
        private readonly IHostedService _service;
        private readonly IApplicationLifetime _applicationLifetime;
        private readonly ILogger<StartCheckDecorator> _logger;

        public StartCheckDecorator(
            IHostedService service,
            IApplicationLifetime applicationLifetime,
            ILogger<StartCheckDecorator> logger)
        {
            _service = service;
            _applicationLifetime = applicationLifetime;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _service.StartAsync(cancellationToken);
                _logger.LogInformation($"Service {_service.GetType().FullName} started");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Error starting hosted service {_service.GetType().FullName}");
                _applicationLifetime.StopApplication(); 
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _service.StopAsync(cancellationToken);
        }
    }
}