namespace DapperKaggleProject.DTOS.GamesDTOS
{
    public class RecentGameDTO
    {
        public DateTime GameDate { get; set; }
        public long HomeTeamId { get; set; }
        public long VisitorTeamId { get; set; }
        public int Season { get; set; }
        public decimal? PtsHome { get; set; }
        public decimal? PtsAway { get; set; }
        public decimal? FgPctHome { get; set; }
        public decimal? FtPctHome { get; set; }
        public decimal? Fg3PctHome { get; set; }
        public decimal? AstHome { get; set; }
        public decimal? RebHome { get; set; }
        public decimal? FgPctAway { get; set; }
        public decimal? FtPctAway { get; set; }
        public decimal? Fg3PctAway { get; set; }
        public decimal? AstAway { get; set; }
        public decimal? RebAway { get; set; }
        public bool HomeTeamWins { get; set; }
        public string HomeTeamAbbreviation { get; set; } = null!;
        public string VisitorTeamAbbreviation { get; set; } = null!;

        public bool IsHomeGame(long teamId) => HomeTeamId == teamId;
        public bool IsAwayGame(long teamId) => VisitorTeamId == teamId;

        public string GetOpponentAbbreviation(long teamId)
        {
            return IsHomeGame(teamId) ? VisitorTeamAbbreviation : HomeTeamAbbreviation;
        }

        public decimal? GetTeamScore(long teamId)
        {
            return IsHomeGame(teamId) ? PtsHome : PtsAway;
        }

        public decimal? GetOpponentScore(long teamId)
        {
            return IsHomeGame(teamId) ? PtsAway : PtsHome;
        }

        public string GetResult(long teamId)
        {
            var teamScore = GetTeamScore(teamId);
            var opponentScore = GetOpponentScore(teamId);
            
            if (!teamScore.HasValue || !opponentScore.HasValue)
                return "N/A";
                
            return teamScore > opponentScore ? "W" : "L";
        }

        public string GetResultColor(long teamId)
        {
            var result = GetResult(teamId);
            return result switch
            {
                "W" => "text-success",
                "L" => "text-danger",
                _ => "text-muted"
            };
        }

        public string GetLocationIndicator(long teamId)
        {
            return IsHomeGame(teamId) ? "vs" : "@";
        }
    }
}
