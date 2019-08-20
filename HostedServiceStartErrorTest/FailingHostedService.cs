using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace HostedServiceStartErrorTest
{
    public class FailingHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.FromException(new Exception("StartAsync"));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.FromException(new Exception("StopAsync"));
        }
    }
}