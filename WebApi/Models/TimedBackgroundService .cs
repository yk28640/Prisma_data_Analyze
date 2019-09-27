using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Services;
namespace WebApi.Models
{
    public class TimedBackgroundService : BackgroundService
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IServiceProvider _provider;
        private int name_flag = 0;
        public TimedBackgroundService(IServiceProvider serviceProvider, ILogger<TimedBackgroundService> logger)
        {
            _logger = logger;
            _provider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            

            
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            _logger.LogInformation("周六!");
            await Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (IServiceScope scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
                _logger.LogInformation($"inser 1 data , - {DateTime.Now}");
                context.TodoItems.Add(new TodoItem { Name = "hsg"+ name_flag++ });
                context.SaveChanges();
            }
           
           

            
        }

        public override void Dispose()
        {
            base.Dispose();
            _timer?.Dispose();
        }
    }
}
