using BankingEvaluation.DbContext;
using BankingEvaluation.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BankingEvaluation
{
    internal class Service : IService
    {
        
        public void Initialize()
        {
            Config = new ConfigurationBuilder()
                .AddJsonFile("Settings.json")
                .Build();
            
            Services = new ServiceCollection()
                .AddDbContext<IUnitOfWork, SqliteContext>()
                .AddSingleton<IConfiguration>(Config)
                .AddTransient<IReportProvider, ReportProvider>()
                .AddTransient<IReportImporter, ReportImporter>()
                .AddLogging(p => p.AddSerilog(new LoggerConfiguration()
                        .ReadFrom.Configuration(Config)
                        .CreateLogger())
                    .SetMinimumLevel(LogLevel.Information));
            
            DbInitializer.Initialize(Services
                .BuildServiceProvider()
                .GetRequiredService<IUnitOfWork>());
        }
        
        public Microsoft.Extensions.Logging.ILogger LoggerFactory<T>() => Provider.GetRequiredService<ILoggerFactory>().CreateLogger<T>();
        
        public IConfiguration Config { get; private set; }

        public IServiceCollection Services { get; private set; }

        public IServiceProvider Provider => Services.BuildServiceProvider();

        public IUnitOfWork UnitOfWork => Provider.GetRequiredService<IUnitOfWork>();
    }

    internal interface IService
    {
        Microsoft.Extensions.Logging.ILogger LoggerFactory<T>();
        
        IConfiguration Config { get; }

        IServiceCollection Services { get; }

        IServiceProvider Provider { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
