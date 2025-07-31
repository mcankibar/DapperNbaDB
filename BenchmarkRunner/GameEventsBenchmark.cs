using BenchmarkDotNet.Attributes;
using DapperKaggleProject.Data;
using DapperKaggleProject.Services.DapperServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DapperKaggleProject.Services
{
    public class GameEventsBenchmark
    {
        private TeamsService _teamsService;
        private string _gameId = "11300001";

        [GlobalSetup]
        public void Setup()
        {
            // Servis ve bağımlılıkları burada oluşturulmalı (gerçek connection string ile)
            var context = new NbadbContext();
            var logger = new LoggerFactory().CreateLogger<TeamsService>();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _teamsService = new TeamsService(config, logger, context);
        }

        [Benchmark]
        public async Task Dapper_GetGameEvents() => await _teamsService.GetGameEventsAsync(_gameId);

        [Benchmark]
        public async Task EFCore_GetGameEvents() => await _teamsService.GetGameEventsWithEFCoreAsync(_gameId);
    }
}
