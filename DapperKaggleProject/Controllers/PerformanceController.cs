using DapperKaggleProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperKaggleProject.Controllers
{
    public class PerformanceController : Controller
    {
        private readonly PerformanceComparisonService _performanceService;
        private readonly ILogger<PerformanceController> _logger;

        public PerformanceController(
            PerformanceComparisonService performanceService, 
            ILogger<PerformanceController> logger)
        {
            _performanceService = performanceService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RunTests()
        {
            try
            {
                _logger.LogInformation("Starting performance comparison tests...");
                
                var basicTests = await _performanceService.RunAllPerformanceTestsAsync(5);
                var largeDataTests = await _performanceService.RunLargeDataTestsAsync(3);

                var allResults = basicTests.Concat(largeDataTests).ToList();

                _logger.LogInformation("Performance tests completed. {Count} tests run.", allResults.Count);

                return Json(new { success = true, results = allResults });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running performance tests");
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RunBasicTests()
        {
            try
            {
                _logger.LogInformation("Starting basic performance tests...");
                
                var results = await _performanceService.RunAllPerformanceTestsAsync(5);

                _logger.LogInformation("Basic performance tests completed. {Count} tests run.", results.Count);

                return Json(new { success = true, results = results });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running basic performance tests");
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RunLargeDataTests()
        {
            try
            {
                _logger.LogInformation("Starting large data performance tests...");
                
                var results = await _performanceService.RunLargeDataTestsAsync(3);

                _logger.LogInformation("Large data performance tests completed. {Count} tests run.", results.Count);

                return Json(new { success = true, results = results });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running large data performance tests");
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
