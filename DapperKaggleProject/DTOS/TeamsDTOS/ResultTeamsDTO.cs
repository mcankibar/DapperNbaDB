namespace DapperKaggleProject.DTOS.TeamsDTOS
{
    public class ResultTeamsDTO
    {
        public long Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Abbreviation { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public int YearFounded { get; set; }

        public string LogoPath => GetLogoPath();

        private string GetLogoPath()
        {
            if (string.IsNullOrEmpty(Abbreviation))
                return "/logos/default.png";

            var logoName = Abbreviation.ToLowerInvariant() switch
            {
                "atl" => "atlanta",
                "bos" => "boston",
                "cle" => "cleveland",
                "nop" => "neworleans",
                "chi" => "chicago",
                "dal" => "dallas",
                "den" => "dever",
                "gsw" => "goldenstate",
                "hou" => "houston",
                "lac" => "clipper",
                "lal" => "lakers",
                "mia" => "miami",
                "mil" => "milwaukee",
                "min" => "minnesota",
                "bkn" => "nets",
                "nyk" => "newyork",
                "orl" => "orlando",
                "ind" => "pacers",
                "phi" => "philadelphia",
                "phx" => "phoenix",
                "por" => "portland",
                "sac" => "sacramento",
                "sas" => "spurs",
                "okc" => "oklahoma",
                "tor" => "toronto",
                "uta" => "utah",
                "mem" => "memphis",
                "was" => "wizards",
                "det" => "detroit",
                "cha" => "charlotte",
                _ => Abbreviation.ToLowerInvariant()
            };

            return $"/logos/{logoName}.png";
        }

        public string DisplayName => $"{City} {Nickname}";
        public string FullDisplayName => $"{FullName} ({Abbreviation})";
        public bool IsWestCoast => State.ToLower() is "california" or "oregon" or "washington";
        public bool IsEastCoast => State.ToLower() is "new york" or "massachusetts" or "florida" or "pennsylvania";
    }
}
