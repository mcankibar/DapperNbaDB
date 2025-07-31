using DapperKaggleProject.DTOS.GamesDTOS;

namespace DapperKaggleProject.Models
{
    public class GameDetailsViewModel
    {
        public string GameId { get; set; } = string.Empty;
        public List<GameEventDTO> GameEvents { get; set; } = new List<GameEventDTO>();
        public Dictionary<int, List<GameEventDTO>> EventsByPeriod { get; set; } = new Dictionary<int, List<GameEventDTO>>();
        public int TotalEvents { get; set; }

        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public string? GameDate { get; set; }
        public string? FinalScore { get; set; }

        
        public string? HomeTeamAbbrev { get; set; }
        public string? HomeTeamName { get; set; }
        public string? HomeTeamCity { get; set; }
        public string? HomeTeamNickname { get; set; }
        public decimal? HomeScore { get; set; }
        public string? HomeTeamLogoPath { get; set; }
        
        public string? AwayTeamAbbrev { get; set; }
        public string? AwayTeamName { get; set; }
        public string? AwayTeamCity { get; set; }
        public string? AwayTeamNickname { get; set; }
        public decimal? AwayScore { get; set; }
        public string? AwayTeamLogoPath { get; set; }

        public int FirstQuarterEvents => EventsByPeriod.ContainsKey(1) ? EventsByPeriod[1].Count : 0;
        public int SecondQuarterEvents => EventsByPeriod.ContainsKey(2) ? EventsByPeriod[2].Count : 0;
        public int ThirdQuarterEvents => EventsByPeriod.ContainsKey(3) ? EventsByPeriod[3].Count : 0;
        public int FourthQuarterEvents => EventsByPeriod.ContainsKey(4) ? EventsByPeriod[4].Count : 0;
        public int OvertimeEvents => EventsByPeriod.Where(kv => kv.Key > 4).Sum(kv => kv.Value.Count);

        public bool HasOvertime => EventsByPeriod.Any(kv => kv.Key > 4);
        public int MaxPeriod => EventsByPeriod.Keys.Any() ? EventsByPeriod.Keys.Max() : 4;
        
        public List<GameEventDTO> ScoringEvents => GameEvents.Where(e => e.IsScoring).ToList();
        public List<GameEventDTO> TurnoversEvents => GameEvents.Where(e => e.IsTurnover).ToList();
        public List<GameEventDTO> SubstitutionEvents => GameEvents.Where(e => e.IsSubstitution).ToList();
        public List<GameEventDTO> FoulEvents => GameEvents.Where(e => e.IsFoul).ToList();

        public string GetPeriodName(int period)
        {
            return period switch
            {
                1 => "1st Quarter",
                2 => "2nd Quarter",
                3 => "3rd Quarter",
                4 => "4th Quarter",
                _ when period > 4 => $"Overtime {period - 4}",
                _ => $"Period {period}"
            };
        }
    }
}
