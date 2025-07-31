using DapperKaggleProject.DTOS.GamesDTOS;

namespace DapperKaggleProject.DTOS.TeamsDTOS
{
    public class TeamDetailDTO
    {
        public long TeamId { get; set; }
        public string Abbreviation { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public int YearFounded { get; set; }
        public string City { get; set; } = null!;
        public string? Arena { get; set; }
        public int? ArenaCapacity { get; set; }
        public string? Owner { get; set; }
        public string? GeneralManager { get; set; }
        public string? HeadCoach { get; set; }
        public string? DLeagueAffiliation { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }

        public string FullName { get; set; } = null!;
        public string? State { get; set; }

        public List<RecentGameDTO> RecentGames { get; set; } = new List<RecentGameDTO>();

        public string LogoPath => GetLogoPath();
        public string DisplayName => $"{City} {Nickname}";
        public string FullDisplayName => $"{FullName} ({Abbreviation})";
        public string ArenaInfo => Arena != null && ArenaCapacity.HasValue 
            ? $"{Arena} ({ArenaCapacity:N0} capacity)" 
            : Arena ?? "N/A";

        private string GetLogoPath()
        {
            if (string.IsNullOrEmpty(Abbreviation))
                return "/logos/default.png";

            
            var logoName = Abbreviation.ToLowerInvariant() switch
            {
                "atl" => "atlanta",           // Atlanta Hawks
                "bos" => "boston",            // Boston Celtics
                "cle" => "cleveland",         // Cleveland Cavaliers
                "nop" => "neworleans",        // New Orleans Pelicans
                "chi" => "chicago",           // Chicago Bulls
                "dal" => "dallas",            // Dallas Mavericks
                "den" => "dever",             // Denver Nuggets
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
                _ => Abbreviation.ToLowerInvariant()   // Fallback to abbreviation
            };

            return $"/logos/{logoName}.png";
        }
    }
}
