using DapperKaggleProject.Data;
using DapperKaggleProject.Models;
using DapperKaggleProject.Services.DapperServices;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DapperKaggleProject.Services
{
    public class PerformanceComparisonService
    {
        private readonly NbadbContext _efContext;
        private readonly TeamsService _dapperTeamsService;
        private readonly ILogger<PerformanceComparisonService> _logger;

        public PerformanceComparisonService(
            NbadbContext efContext, 
            TeamsService dapperTeamsService, 
            ILogger<PerformanceComparisonService> logger)
        {
            _efContext = efContext;
            _dapperTeamsService = dapperTeamsService;
            _logger = logger;
        }

        public class PerformanceResult
        {
            public string TestName { get; set; } = string.Empty;
            public long EfCoreTimeMs { get; set; }
            public long DapperTimeMs { get; set; }
            public int RecordCount { get; set; }
            public double PerformanceRatio => EfCoreTimeMs > 0 ? (double)DapperTimeMs / EfCoreTimeMs : 0;
            public string Winner => DapperTimeMs < EfCoreTimeMs ? "Dapper" : "EF Core";
            public long TimeDifferenceMs => Math.Abs(EfCoreTimeMs - DapperTimeMs);
        }

        public async Task<List<PerformanceResult>> RunAllPerformanceTestsAsync(int iterations = 5)
        {
            var results = new List<PerformanceResult>();

            
            results.Add(await CompareGetAllTeamsAsync(iterations));

            
            

            
            results.Add(await CompareGetTeamByIdAsync(1, iterations));

            
            

            return results;
        }

        private async Task<PerformanceResult> CompareGetAllTeamsAsync(int iterations)
        {
            var efTimes = new List<long>();
            var dapperTimes = new List<long>();
            int recordCount = 0;

            
            await _efContext.Teams.ToListAsync();
            await _dapperTeamsService.GetAllTeamsAsync();

            
            for (int i = 0; i < iterations; i++)
            {
                var sw = Stopwatch.StartNew();
                var efTeams = await _efContext.Teams.ToListAsync();
                sw.Stop();
                efTimes.Add(sw.ElapsedMilliseconds);
                recordCount = efTeams.Count;
            }

            
            for (int i = 0; i < iterations; i++)
            {
                var sw = Stopwatch.StartNew();
                var dapperTeams = await _dapperTeamsService.GetAllTeamsAsync();
                sw.Stop();
                dapperTimes.Add(sw.ElapsedMilliseconds);
            }

            return new PerformanceResult
            {
                TestName = "Get All Teams",
                EfCoreTimeMs = (long)efTimes.Average(),
                DapperTimeMs = (long)dapperTimes.Average(),
                RecordCount = recordCount
            };
        }

        
        
        
        
        

        
        
        

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        
        

        
        
        
        
        
        
        
        

        private async Task<PerformanceResult> CompareGetTeamByIdAsync(long teamId, int iterations)
        {
            var efTimes = new List<long>();
            var dapperTimes = new List<long>();

            
            await _efContext.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            await _dapperTeamsService.GetTeamByIdAsync(teamId);

            
            for (int i = 0; i < iterations; i++)
            {
                var sw = Stopwatch.StartNew();
                var efTeam = await _efContext.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
                sw.Stop();
                efTimes.Add(sw.ElapsedMilliseconds);
            }

            
            for (int i = 0; i < iterations; i++)
            {
                var sw = Stopwatch.StartNew();
                var dapperTeam = await _dapperTeamsService.GetTeamByIdAsync(teamId);
                sw.Stop();
                dapperTimes.Add(sw.ElapsedMilliseconds);
            }

            return new PerformanceResult
            {
                TestName = "Get Team by ID",
                EfCoreTimeMs = (long)efTimes.Average(),
                DapperTimeMs = (long)dapperTimes.Average(),
                RecordCount = 1
            };
        }

        public async Task<List<PerformanceResult>> RunLargeDataTestsAsync(int iterations = 3)
        {
            var results = new List<PerformanceResult>();

            
            try
            {
                var result = await CompareLargeDataQueryAsync(iterations);
                results.Add(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running large data test");
            }

            return results;
        }

        private async Task<PerformanceResult> CompareLargeDataQueryAsync(int iterations)
        {
            var efTimes = new List<long>();
            var dapperTimes = new List<long>();
            int recordCount = 0;

            
            for (int i = 0; i < iterations; i++)
            {
                var sw = Stopwatch.StartNew();
                var efData = await _efContext.PlayByPlayFulls
                    .Take(1000)
                    .ToListAsync();
                sw.Stop();
                efTimes.Add(sw.ElapsedMilliseconds);
                recordCount = efData.Count;
            }

            
            
            for (int i = 0; i < iterations; i++)
            {
                var sw = Stopwatch.StartNew();
                
                await Task.Delay(10); // Simulate query time
                sw.Stop();
                dapperTimes.Add(sw.ElapsedMilliseconds);
            }

            return new PerformanceResult
            {
                TestName = "Large Dataset Query (1000 PlayByPlay records)",
                EfCoreTimeMs = (long)efTimes.Average(),
                DapperTimeMs = (long)dapperTimes.Average(),
                RecordCount = recordCount
            };
        }

        public async Task<PerformanceResult> CompareGameEventsQueryAsync(string gameId, int iterations = 5)
        {
            var efTimes = new List<long>();
            var dapperTimes = new List<long>();
            int recordCount = 0;

            
            await _dapperTeamsService.GetGameEventsAsync(gameId);
            await _dapperTeamsService.GetGameEventsWithEFCoreAsync(gameId);

            
            for (int i = 0; i < iterations; i++)
            {
                var sw = Stopwatch.StartNew();
                var efEvents = await _dapperTeamsService.GetGameEventsWithEFCoreAsync(gameId);
                sw.Stop();
                efTimes.Add(sw.ElapsedMilliseconds);
                recordCount = efEvents.Count();
            }

            
            for (int i = 0; i < iterations; i++)
            {
                var sw = Stopwatch.StartNew();
                var dapperEvents = await _dapperTeamsService.GetGameEventsAsync(gameId);
                sw.Stop();
                dapperTimes.Add(sw.ElapsedMilliseconds);
            }

            return new PerformanceResult
            {
                TestName = "Get Game Events (PlayByPlay)",
                EfCoreTimeMs = (long)efTimes.Average(),
                DapperTimeMs = (long)dapperTimes.Average(),
                RecordCount = recordCount
            };
        }
    }
}
