using Dapper;
using DapperKaggleProject.Data;
using DapperKaggleProject.DTOS.GamesDTOS;
using DapperKaggleProject.DTOS.TeamsDTOS;
using DapperKaggleProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DapperKaggleProject.Services.DapperServices
{
    public class TeamsService
    {
        private readonly string _connectionString;

        private readonly NbadbContext context;
        private readonly ILogger<TeamsService> _logger;

        public TeamsService(IConfiguration configuration, ILogger<TeamsService> logger, NbadbContext context)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null");
            _logger = logger;
            this.context = context;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }


        public async Task<IEnumerable<ResultTeamsDTO>> GetAllTeamsAsync()
        {
            try
            {
                using var connection = CreateConnection();
                const string sql = @"
                    SELECT 
                        id AS Id,
                        full_name AS FullName,
                        abbreviation AS Abbreviation,
                        nickname AS Nickname,
                        city AS City,
                        state AS State,
                        year_founded AS YearFounded
                    FROM team 
                    WHERE full_name NOT LIKE 'Historical%'
                    ORDER BY full_name";

                var teams = await connection.QueryAsync<ResultTeamsDTO>(sql);
                _logger.LogInformation($"Retrieved {teams.Count()} teams from database (excluding historical teams)");
                return teams;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teams from database");
                throw;
            }
        }


        public async Task<Team?> GetTeamByIdAsync(long id)
        {
            try
            {
                using var connection = CreateConnection();
                const string sql = @"
                    SELECT 
                        id AS Id, 
                        full_name AS FullName, 
                        abbreviation AS Abbreviation, 
                        nickname AS Nickname, 
                        city AS City, 
                        state AS State, 
                        year_founded AS YearFounded
                    FROM team 
                    WHERE id = @Id AND full_name NOT LIKE 'Historical%'";

                var team = await connection.QueryFirstOrDefaultAsync<Team>(sql, new { Id = id });
                
                if (team != null)
                {
                    _logger.LogInformation($"Retrieved team: {team.FullName}");
                }
                else
                {
                    _logger.LogWarning($"Team with ID {id} not found");
                }

                return team;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving team with ID {id}");
                throw;
            }
        }


        public async Task<Team?> GetTeamByAbbreviationAsync(string abbreviation)
        {
            try
            {
                using var connection = CreateConnection();
                const string sql = @"
                    SELECT 
                        id AS Id, 
                        full_name AS FullName, 
                        abbreviation AS Abbreviation, 
                        nickname AS Nickname, 
                        city AS City, 
                        state AS State, 
                        year_founded AS YearFounded
                    FROM team 
                    WHERE abbreviation = @Abbreviation AND full_name NOT LIKE 'Historical%'";

                var team = await connection.QueryFirstOrDefaultAsync<Team>(sql, new { Abbreviation = abbreviation });
                
                if (team != null)
                {
                    _logger.LogInformation($"Retrieved team by abbreviation: {team.FullName}");
                }

                return team;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving team with abbreviation {abbreviation}");
                throw;
            }
        }


        public async Task<IEnumerable<Team>> GetTeamsByConferenceAsync(string conference)
        {
            try
            {
                using var connection = CreateConnection();
                

                const string sql = @"
                    SELECT DISTINCT
                        t.id AS Id, 
                        t.full_name AS FullName, 
                        t.abbreviation AS Abbreviation, 
                        t.nickname AS Nickname, 
                        t.city AS City, 
                        t.state AS State, 
                        t.year_founded AS YearFounded
                    FROM team t
                    LEFT JOIN team_details td ON t.id = td.TeamId
                    WHERE td.Conference = @Conference AND t.full_name NOT LIKE 'Historical%'
                    ORDER BY t.full_name";

                var teams = await connection.QueryAsync<Team>(sql, new { Conference = conference });
                _logger.LogInformation($"Retrieved {teams.Count()} teams from {conference} conference");
                return teams;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving teams from {conference} conference");
                throw;
            }
        }


        public async Task<object> GetTeamStatsAsync(long teamId, int? season = null)
        {
            
                using var connection = CreateConnection();
                
                var sql = @"
                    SELECT 
                        COUNT(DISTINCT g.Id) as TotalGames,
                        SUM(CASE WHEN g.TeamIdHome = @TeamId AND g.PtsHome > g.PtsAway 
                                  OR g.TeamIdAway = @TeamId AND g.PtsAway > g.PtsHome 
                                  THEN 1 ELSE 0 END) as Wins,
                        SUM(CASE WHEN g.TeamIdHome = @TeamId AND g.PtsHome < g.PtsAway 
                                  OR g.TeamIdAway = @TeamId AND g.PtsAway < g.PtsHome 
                                  THEN 1 ELSE 0 END) as Losses,
                        AVG(CASE WHEN g.TeamIdHome = @TeamId THEN g.PtsHome 
                                 WHEN g.TeamIdAway = @TeamId THEN g.PtsAway END) as AvgPoints,
                        AVG(CASE WHEN g.TeamIdHome = @TeamId THEN g.PtsAway 
                                 WHEN g.TeamIdAway = @TeamId THEN g.PtsHome END) as AvgPointsAllowed
                    FROM Games g
                    WHERE (g.TeamIdHome = @TeamId OR g.TeamIdAway = @TeamId)";

                if (season.HasValue)
                {
                    sql += " AND YEAR(g.GameDate) = @Season";
                }

                var stats = await connection.QueryFirstOrDefaultAsync(sql, new { TeamId = teamId, Season = season });
                return stats ?? new { };
            
        }


        public string GetTeamLogoPath(string abbreviation)
        {
            if (string.IsNullOrEmpty(abbreviation))
                return "/logos/default.png";


            var logoName = abbreviation.ToLower() switch
            {
                "atl" => "atlanta",           // Atlanta Hawks
                "bos" => "boston",            // Boston Celtics
                "cle" => "cleveland",         // Cleveland Cavaliers
                "nop" => "neworleans",        // New Orleans Pelicans
                "chi" => "chicago",           // Chicago Bulls
                "dal" => "dallas",            // Dallas Mavericks
                "den" => "denver",             // Denver Nuggets
                "gsw" => "goldenstate",       // Golden State Warriors
                "hou" => "houston",           // Houston Rockets
                "lac" => "clipper",           // Los Angeles Clippers
                "lal" => "lakers",            // Los Angeles Lakers
                "mia" => "miami",             // Miami Heat
                "mil" => "milwaukee",         // Milwaukee Bucks
                "min" => "minnesota",         // Minnesota Timberwolves
                "bkn" => "nets",              // Brooklyn Nets
                "nyk" => "newyork",           // New York Knicks
                "orl" => "orlando",           // Orlando Magic
                "ind" => "pacers",            // Indiana Pacers
                "phi" => "philadelphia",      // Philadelphia 76ers
                "phx" => "phoenix",           // Phoenix Suns
                "por" => "portland",          // Portland Trail Blazers
                "sac" => "sacramento",        // Sacramento Kings
                "sas" => "spurs",             // San Antonio Spurs
                "okc" => "oklahoma",          // Oklahoma City Thunder
                "tor" => "toronto",           // Toronto Raptors
                "uta" => "utah",              // Utah Jazz
                "mem" => "memphis",           // Memphis Grizzlies
                "was" => "wizards",           // Washington Wizards
                "det" => "detroit",           // Detroit Pistons
                "cha" => "charlotte",         // Charlotte Hornets
                _ => abbreviation.ToLower()
            };

            return $"/logos/{logoName}.png";
        }


        public async Task<TeamDetailDTO?> GetTeamDetailAsync(int teamId)
        {
            try
            {
                using var connection = CreateConnection();
                

                const string teamDetailSql = @"
                    SELECT 
                        td.team_id AS TeamId,
                        t.abbreviation AS Abbreviation,
                        t.full_name AS FullName,
                        t.nickname AS Nickname,
                        t.city AS City,
                        t.state AS State,
                        t.year_founded AS YearFounded,
                        td.arena AS Arena,
                        td.arenacapacity AS ArenaCapacity,
                        td.owner AS Owner,
                        td.generalmanager AS GeneralManager,
                        td.headcoach AS HeadCoach,
                        td.dleagueaffiliation AS DLeagueAffiliation,
                        td.facebook AS Facebook,
                        td.instagram AS Instagram,
                        td.twitter AS Twitter
                    FROM team_details td
                    INNER JOIN team t ON td.team_id = t.id
                    WHERE td.team_id = @TeamId";

                var teamDetail = await connection.QueryFirstOrDefaultAsync<TeamDetailDTO>(teamDetailSql, new { TeamId = teamId });
                
                if (teamDetail == null)
                {
                    _logger.LogWarning($"Team detail not found for team ID: {teamId}");
                    return null;
                }


                const string recentGamesSql = @"
                    SELECT TOP 10
                        g.game_date AS GameDate,
                        g.team_id_home AS HomeTeamId,
                        g.team_id_away AS VisitorTeamId,
                        g.season_id AS Season,
                        g.pts_home AS PtsHome,
                        g.fg_pct_home AS FgPctHome,
                        g.ft_pct_home AS FtPctHome,
                        g.fg3_pct_home AS Fg3PctHome,
                        g.ast_home AS AstHome,
                        g.reb_home AS RebHome,
                        g.pts_away AS PtsAway,
                        g.fg_pct_away AS FgPctAway,
                        g.ft_pct_away AS FtPctAway,
                        g.fg3_pct_away AS Fg3PctAway,
                        g.ast_away AS AstAway,
                        g.reb_away AS RebAway,
                        CASE WHEN g.pts_home > g.pts_away THEN 1 ELSE 0 END AS HomeTeamWins,
                        g.team_abbreviation_home AS HomeTeamAbbreviation,
                        g.team_abbreviation_away AS VisitorTeamAbbreviation
                    FROM game g
                    WHERE g.team_id_home = @TeamId OR g.team_id_away = @TeamId
                    ORDER BY g.game_date DESC";

                var recentGames = await connection.QueryAsync<RecentGameDTO>(recentGamesSql, new { TeamId = teamId });
                teamDetail.RecentGames = recentGames.ToList();

                _logger.LogInformation($"Retrieved team detail for team ID {teamId} with {recentGames.Count()} recent games");
                return teamDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting team detail for team ID: {teamId}");
                throw;
            }
        }


        public async Task<IEnumerable<RecentGameDTO>> GetTeamGamesAsync(int teamId, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                using var connection = CreateConnection();
                
                var whereClause = "WHERE (g.team_id_home = @TeamId OR g.team_id_away = @TeamId)";
                object parameters = new { TeamId = teamId };
                
                if (startDate.HasValue && endDate.HasValue)
                {
                    whereClause += " AND g.game_date BETWEEN @StartDate AND @EndDate";
                    parameters = new { TeamId = teamId, StartDate = startDate.Value, EndDate = endDate.Value };
                }
                else if (startDate.HasValue)
                {
                    whereClause += " AND g.game_date >= @StartDate";
                    parameters = new { TeamId = teamId, StartDate = startDate.Value };
                }

                var sql = $@"
                    SELECT 
                        g.game_date AS GameDate,
                        g.team_id_home AS HomeTeamId,
                        g.team_id_away AS VisitorTeamId,
                        g.season_id AS Season,
                        g.pts_home AS PtsHome,
                        g.fg_pct_home AS FgPctHome,
                        g.ft_pct_home AS FtPctHome,
                        g.fg3_pct_home AS Fg3PctHome,
                        g.ast_home AS AstHome,
                        g.reb_home AS RebHome,
                        g.pts_away AS PtsAway,
                        g.fg_pct_away AS FgPctAway,
                        g.ft_pct_away AS FtPctAway,
                        g.fg3_pct_away AS Fg3PctAway,
                        g.ast_away AS AstAway,
                        g.reb_away AS RebAway,
                        CASE WHEN g.pts_home > g.pts_away THEN 1 ELSE 0 END AS HomeTeamWins,
                        g.team_abbreviation_home AS HomeTeamAbbreviation,
                        g.team_abbreviation_away AS VisitorTeamAbbreviation
                    FROM game g
                    {whereClause}
                    ORDER BY g.game_date DESC";

                var games = await connection.QueryAsync<RecentGameDTO>(sql, parameters);
                _logger.LogInformation($"Retrieved {games.Count()} games for team ID {teamId}");
                return games;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting games for team ID: {teamId}");
                throw;
            }
        }


        public async Task<GetGamesByTeamIdDTO> GetGamesByTeamIdAsync(int teamId, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int pageSize = 20, string? seasonType = null)
        {
            try
            {
                using var connection = CreateConnection();
                

                var whereConditions = new List<string> { "(g.team_id_home = @TeamId OR g.team_id_away = @TeamId)" };
                var parametersList = new List<(string, object)> { ("TeamId", teamId) };
                
                if (startDate.HasValue)
                {
                    whereConditions.Add("g.game_date >= @StartDate");
                    parametersList.Add(("StartDate", startDate.Value));
                }
                
                if (endDate.HasValue)
                {
                    whereConditions.Add("g.game_date <= @EndDate");
                    parametersList.Add(("EndDate", endDate.Value));
                }
                
                if (!string.IsNullOrEmpty(seasonType) && seasonType != "All")
                {
                    whereConditions.Add("g.season_type = @SeasonType");
                    parametersList.Add(("SeasonType", seasonType));
                }
                
                var whereClause = string.Join(" AND ", whereConditions);
                

                var parameters = new DynamicParameters();
                foreach (var (name, value) in parametersList)
                {
                    parameters.Add(name, value);
                }
                

                var countSql = $@"
                    SELECT COUNT(*)
                    FROM game g
                    WHERE {whereClause}";
                
                var totalCount = await connection.QuerySingleAsync<int>(countSql, parameters);
                

                var offset = (page - 1) * pageSize;
                var gamesSql = $@"
                    SELECT 
                        g.game_id AS GameId,
                        g.game_date AS GameDate,
                        g.season_type AS SeasonType,
                        g.season_id AS SeasonId,
                        
                        -- Home Team Info
                        g.team_id_home AS TeamIdHome,
                        g.team_abbreviation_home AS TeamAbbreviationHome,
                        g.team_name_home AS TeamNameHome,
                        g.matchup_home AS MatchupHome,
                        g.wl_home AS WlHome,
                        g.pts_home AS PtsHome,
                        
                        -- Away Team Info
                        g.team_id_away AS TeamIdAway,
                        g.team_abbreviation_away AS TeamAbbreviationAway,
                        g.team_name_away AS TeamNameAway,
                        g.matchup_away AS MatchupAway,
                        g.wl_away AS WlAway,
                        g.pts_away AS PtsAway,
                        
                        -- Home Team Stats
                        g.fgm_home AS FgmHome,
                        g.fga_home AS FgaHome,
                        g.fg_pct_home AS FgPctHome,
                        g.fg3m_home AS Fg3mHome,
                        g.fg3a_home AS Fg3aHome,
                        g.fg3_pct_home AS Fg3PctHome,
                        g.ftm_home AS FtmHome,
                        g.fta_home AS FtaHome,
                        g.ft_pct_home AS FtPctHome,
                        g.reb_home AS RebHome,
                        g.ast_home AS AstHome,
                        g.stl_home AS StlHome,
                        g.blk_home AS BlkHome,
                        g.tov_home AS TovHome,
                        g.pf_home AS PfHome,
                        g.plus_minus_home AS PlusMinusHome,
                        
                        -- Away Team Stats
                        g.fgm_away AS FgmAway,
                        g.fga_away AS FgaAway,
                        g.fg_pct_away AS FgPctAway,
                        g.fg3m_away AS Fg3mAway,
                        g.fg3a_away AS Fg3aAway,
                        g.fg3_pct_away AS Fg3PctAway,
                        g.ftm_away AS FtmAway,
                        g.fta_away AS FtaAway,
                        g.ft_pct_away AS FtPctAway,
                        g.reb_away AS RebAway,
                        g.ast_away AS AstAway,
                        g.stl_away AS StlAway,
                        g.blk_away AS BlkAway,
                        g.tov_away AS TovAway,
                        g.pf_away AS PfAway,
                        g.plus_minus_away AS PlusMinusAway
                        
                    FROM game g
                    WHERE {whereClause}
                    ORDER BY g.game_date DESC
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                

                parameters.Add("Offset", offset);
                parameters.Add("PageSize", pageSize);
                
                var games = await connection.QueryAsync<GetGamesByTeamIdDTO.GameDto>(gamesSql, parameters);
                

                var pagination = new GetGamesByTeamIdDTO.GamesPaginationDTO
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                    HasPreviousPage = page > 1,
                    HasNextPage = page < (int)Math.Ceiling((double)totalCount / pageSize)
                };
                
                return new GetGamesByTeamIdDTO
                {
                    TeamId = teamId,
                    Games = games.ToList(),
                    Pagination = pagination,
                    FilterStartDate = startDate,
                    FilterEndDate = endDate,
                    FilterSeasonType = seasonType
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting games for team ID: {teamId}");
                throw;
            }
        }


        public async Task<IEnumerable<GameEventDTO>> GetGameEventsAsync(string gameId)
        {
            try
            {
                using var connection = CreateConnection();
                const string sql = @"
                    SELECT 
                        game_id AS GameId,
                        eventnum AS EventNum,
                        eventmsgtype AS EventMsgType,
                        eventmsgactiontype AS EventMsgActionType,
                        period AS Period,
                        wctimestring AS WcTimeString,
                        pctimestring AS PcTimeString,
                        homedescription AS HomeDescription,
                        neutraldescription AS NeutralDescription,
                        visitordescription AS VisitorDescription,
                        score AS Score,
                        scoremargin AS ScoreMargin,
                        person1type AS Person1Type,
                        player1_id AS Player1Id,
                        player1_name AS Player1Name,
                        player1_team_id AS Player1TeamId,
                        player1_team_city AS Player1TeamCity,
                        player1_team_nickname AS Player1TeamNickname,
                        player1_team_abbreviation AS Player1TeamAbbreviation,
                        person2type AS Person2Type,
                        player2_id AS Player2Id,
                        player2_name AS Player2Name,
                        player2_team_id AS Player2TeamId,
                        player2_team_city AS Player2TeamCity,
                        player2_team_nickname AS Player2TeamNickname,
                        player2_team_abbreviation AS Player2TeamAbbreviation,
                        person3type AS Person3Type,
                        player3_id AS Player3Id,
                        player3_name AS Player3Name,
                        player3_team_id AS Player3TeamId,
                        player3_team_city AS Player3TeamCity,
                        player3_team_nickname AS Player3TeamNickname,
                        player3_team_abbreviation AS Player3TeamAbbreviation,
                        video_available_flag AS VideoAvailableFlag
                    FROM play_by_play_full 
                    WHERE game_id = @gameId
                    ORDER BY eventnum";

                var events = await connection.QueryAsync<GameEventDTO>(sql, new { gameId });
                _logger.LogInformation($"Retrieved {events.Count()} events for game {gameId}");
                return events;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving game events for game {gameId}");
                throw;
            }
        }


        public async Task<IEnumerable<GameEventDTO>> GetGameEventsWithEFCoreAsync(string gameId)
        {
            try
            {
                var events = await context.PlayByPlayFulls
                    .AsNoTracking()
                    .Where(pbp => pbp.GameId == gameId)
                    .OrderBy(pbp => pbp.Eventnum)
                    .Select(pbp => new GameEventDTO
                    {
                        GameId = pbp.GameId,
                        EventNum = pbp.Eventnum,
                        EventMsgType = pbp.Eventmsgtype,
                        EventMsgActionType = pbp.Eventmsgactiontype,
                        Period = pbp.Period,
                        WcTimeString = pbp.Wctimestring,
                        PcTimeString = pbp.Pctimestring,
                        HomeDescription = pbp.Homedescription,
                        NeutralDescription = pbp.Neutraldescription,
                        VisitorDescription = pbp.Visitordescription,
                        Score = pbp.Score,
                        ScoreMargin = pbp.Scoremargin,
                        Person1Type = (decimal?)pbp.Person1type,
                        Player1Id = pbp.Player1Id,
                        Player1Name = pbp.Player1Name,
                        Player1TeamId = !string.IsNullOrEmpty(pbp.Player1TeamId) ? decimal.Parse(pbp.Player1TeamId) : null,
                        Player1TeamCity = pbp.Player1TeamCity,
                        Player1TeamNickname = pbp.Player1TeamNickname,
                        Player1TeamAbbreviation = pbp.Player1TeamAbbreviation,
                        Person2Type = (decimal?)pbp.Person2type,
                        Player2Id = pbp.Player2Id,
                        Player2Name = pbp.Player2Name,
                        Player2TeamId = !string.IsNullOrEmpty(pbp.Player2TeamId) ? decimal.Parse(pbp.Player2TeamId) : null,
                        Player2TeamCity = pbp.Player2TeamCity,
                        Player2TeamNickname = pbp.Player2TeamNickname,
                        Player2TeamAbbreviation = pbp.Player2TeamAbbreviation,
                        Person3Type = (decimal?)pbp.Person3type,
                        Player3Id = pbp.Player3Id,
                        Player3Name = pbp.Player3Name,
                        Player3TeamId = !string.IsNullOrEmpty(pbp.Player3TeamId) ? decimal.Parse(pbp.Player3TeamId) : null,
                        Player3TeamCity = pbp.Player3TeamCity,
                        Player3TeamNickname = pbp.Player3TeamNickname,
                        Player3TeamAbbreviation = pbp.Player3TeamAbbreviation,
                        VideoAvailableFlag = pbp.VideoAvailableFlag
                    })
                    .ToListAsync();

                _logger.LogInformation($"Retrieved {events.Count()} events for game {gameId} using EF Core");
                return events;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving game events for game {gameId} using EF Core");
                throw;
            }
        }


        public async Task<dynamic?> GetGameInfoAsync(string gameId)
        {
            try
            {
                using var connection = CreateConnection();
                const string sql = @"
                    SELECT 
                        g.game_id AS GameId,
                        g.game_date AS GameDate,
                        g.team_id_home AS HomeTeamId,
                        g.team_id_away AS AwayTeamId,
                        g.pts_home AS HomeScore,
                        g.pts_away AS AwayScore,
                        ht.abbreviation AS HomeTeamAbbrev,
                        ht.full_name AS HomeTeamName,
                        ht.city AS HomeTeamCity,
                        ht.nickname AS HomeTeamNickname,
                        at.abbreviation AS AwayTeamAbbrev,
                        at.full_name AS AwayTeamName,
                        at.city AS AwayTeamCity,
                        at.nickname AS AwayTeamNickname
                    FROM game g
                    INNER JOIN team ht ON g.team_id_home = ht.id
                    INNER JOIN team at ON g.team_id_away = at.id
                    WHERE g.game_id = @gameId";

                var gameInfo = await connection.QueryFirstOrDefaultAsync(sql, new { gameId });
                _logger.LogInformation($"Retrieved game info for game {gameId}");
                return gameInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving game info for game {gameId}");
                throw;
            }
        }


        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                using var connection = CreateConnection();
                await connection.OpenAsync();
                _logger.LogInformation("Database connection test successful");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database connection test failed");
                return false;
            }
        }
    }
}
