using DapperKaggleProject.Data;
using DapperKaggleProject.Models;
using DapperKaggleProject.Services;
using DapperKaggleProject.Services.DapperServices;
using DapperKaggleProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DapperKaggleProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly NbadbContext _context;
        private readonly TeamsService _teamsService;
        private readonly ILogger<AdminController> _logger;

        private readonly PerformanceComparisonService _perfService;

        public AdminController(NbadbContext context, TeamsService teamsService, ILogger<AdminController> logger, PerformanceComparisonService perfService)
        {
            _context = context;
            _teamsService = teamsService;
            _logger = logger;
            _perfService = perfService;
        }

        public async Task<IActionResult> Index()
        {
            var totalPlayers = await _context.Players.CountAsync();
            var totalTeams = await _context.Teams.CountAsync();
            var totalGames = await _context.Games.CountAsync();
            var activePlayers = await _context.Players.Where(p => p.IsActive == 1).CountAsync();

            ViewBag.TotalPlayers = totalPlayers;
            ViewBag.TotalTeams = totalTeams;
            ViewBag.TotalGames = totalGames;
            ViewBag.ActivePlayers = activePlayers;

            return View();
        }

        public IActionResult EfCore()
        {
            return View();
        }

        public IActionResult Dapper()
        {
            return View();
        }

        public async Task<IActionResult> Teams()
        {
            try
            {
                var teams = await _teamsService.GetAllTeamsAsync();
                _logger.LogInformation($"Retrieved {teams.Count()} teams for admin panel");
                return View(teams);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading teams in admin panel");
                TempData["Error"] = "Error loading teams: " + ex.Message;
                return View(new List<DapperKaggleProject.DTOS.TeamsDTOS.ResultTeamsDTO>());
            }
        }

        public async Task<IActionResult> TeamDetails(int id, int? year = null, int? month = null, string? seasonType = null)
        {
            try
            {
                var teamDetail = await _teamsService.GetTeamDetailAsync(id);
                if (teamDetail == null)
                {
                    TempData["Error"] = "Team not found";
                    return RedirectToAction(nameof(Teams));
                }

                var selectedSeasonType = seasonType ?? "All";

                var allGamesData = await _teamsService.GetGamesByTeamIdAsync(id, new DateTime(2000, 1, 1), DateTime.Now.AddYears(1), 1, 10000, selectedSeasonType);
                
                int selectedYear, selectedMonth;
                if (year == null || month == null)
                {
                    if (allGamesData.Games.Any())
                    {
                        var latestGame = allGamesData.Games.OrderByDescending(g => g.GameDate).First();
                        selectedYear = year ?? latestGame.GameDate.Year;
                        selectedMonth = month ?? latestGame.GameDate.Month;
                    }
                    else
                    {
                        selectedYear = year ?? DateTime.Now.Year;
                        selectedMonth = month ?? DateTime.Now.Month;
                    }
                }
                else
                {
                    selectedYear = year.Value;
                    selectedMonth = month.Value;
                }

                var availableYears = allGamesData.Games.Any() 
                    ? allGamesData.Games.Select(g => g.GameDate.Year).Distinct().OrderByDescending(y => y).ToList()
                    : new List<int> { DateTime.Now.Year };
                
                var viewModel = new TeamDetailWithGamesViewModel
                {
                    TeamDetail = teamDetail,
                    GamesData = allGamesData, // All games, filtering will happen in the view
                    SelectedYear = selectedYear,
                    SelectedMonth = selectedMonth,
                    SeasonType = selectedSeasonType,
                    CurrentPage = 1, // Not used anymore
                    AvailableYears = availableYears
                };
                
                _logger.LogInformation($"Retrieved team detail with all games for ID {id}: {teamDetail.FullName} - {allGamesData.Games.Count} total games, showing {selectedMonth}/{selectedYear}");
                return View("TeamDetailsWithCalendar", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading team details for ID {id}");
                TempData["Error"] = "Error loading team details: " + ex.Message;
                return RedirectToAction(nameof(Teams));
            }
        }

        public async Task<IActionResult> TeamDetailsSimple(int id)
        {
            try
            {
                var teamDetail = await _teamsService.GetTeamDetailAsync(id);
                if (teamDetail == null)
                {
                    TempData["Error"] = "Team not found";
                    return RedirectToAction(nameof(Teams));
                }

                _logger.LogInformation($"Retrieved simple team detail for ID {id}: {teamDetail.FullName}");
                return View("TeamDetails", teamDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading simple team details for ID {id}");
                TempData["Error"] = "Error loading team details: " + ex.Message;
                return RedirectToAction(nameof(Teams));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamGames(int teamId, string? startDate = null, string? endDate = null)
        {
            try
            {
                DateTime? start = string.IsNullOrEmpty(startDate) ? null : DateTime.Parse(startDate);
                DateTime? end = string.IsNullOrEmpty(endDate) ? null : DateTime.Parse(endDate);

                var games = await _teamsService.GetTeamGamesAsync(teamId, start, end);
                return Json(games);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting games for team ID {teamId}");
                return Json(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                var isConnected = await _teamsService.TestConnectionAsync();
                return Json(new { success = isConnected, message = isConnected ? "Connection successful" : "Connection failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Connection test error");
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> GameDetails(string gameId)
        {
            try
            {
                if (string.IsNullOrEmpty(gameId))
                {
                    TempData["Error"] = "Game ID is required";
                    return RedirectToAction(nameof(Teams));
                }

                var gameInfo = await _teamsService.GetGameInfoAsync(gameId);
                if (gameInfo == null)
                {
                    TempData["Error"] = "Game not found";
                    return RedirectToAction(nameof(Teams));
                }

                var gameEvents = await _teamsService.GetGameEventsAsync(gameId);
                
                if (!gameEvents.Any())
                {
                    TempData["Error"] = "No game events found for this game";
                    return RedirectToAction(nameof(Teams));
                }

                var eventsByPeriod = gameEvents.GroupBy(e => e.Period).ToDictionary(g => g.Key, g => g.ToList());
                
                var viewModel = new GameDetailsViewModel
                {
                    GameId = gameId,
                    GameEvents = gameEvents.ToList(),
                    EventsByPeriod = eventsByPeriod,
                    TotalEvents = gameEvents.Count(),
                    
                    GameDate = ((DateTime)gameInfo.GameDate).ToString("MMMM dd, yyyy", new System.Globalization.CultureInfo("en-US")),
                    
                    HomeTeamAbbrev = gameInfo.HomeTeamAbbrev,
                    HomeTeamName = gameInfo.HomeTeamName,
                    HomeTeamCity = gameInfo.HomeTeamCity,
                    HomeTeamNickname = gameInfo.HomeTeamNickname,
                    HomeScore = gameInfo.HomeScore,
                    HomeTeamLogoPath = _teamsService.GetTeamLogoPath(gameInfo.HomeTeamAbbrev),
                    
                    AwayTeamAbbrev = gameInfo.AwayTeamAbbrev,
                    AwayTeamName = gameInfo.AwayTeamName,
                    AwayTeamCity = gameInfo.AwayTeamCity,
                    AwayTeamNickname = gameInfo.AwayTeamNickname,
                    AwayScore = gameInfo.AwayScore,
                    AwayTeamLogoPath = _teamsService.GetTeamLogoPath(gameInfo.AwayTeamAbbrev),
                    
                    FinalScore = $"{gameInfo.HomeScore} - {gameInfo.AwayScore}"
                };

                _logger.LogInformation($"Retrieved game details for game {gameId}: {gameEvents.Count()} events");
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading game details for game {gameId}");
                TempData["Error"] = "Error loading game details: " + ex.Message;
                return RedirectToAction(nameof(Teams));
            }
        }

        public async Task<IActionResult> PerformanceTest(string gameId)
        {
            try
            {
                var result = await _perfService.CompareGameEventsQueryAsync(gameId);
                ViewBag.PerformanceResult = result;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PerformanceTest error");
                TempData["Error"] = "Performance test failed: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
