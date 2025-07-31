using DapperKaggleProject.DTOS.TeamsDTOS;
using DapperKaggleProject.DTOS.GamesDTOS;

namespace DapperKaggleProject.ViewModels
{
    public class TeamDetailWithGamesViewModel
    {
        
        public TeamDetailDTO TeamDetail { get; set; } = null!;
        
        
        public GetGamesByTeamIdDTO GamesData { get; set; } = null!;
        
        
        public int SelectedYear { get; set; } = DateTime.Now.Year;
        public int SelectedMonth { get; set; } = DateTime.Now.Month;
        
        
        public string? SeasonType { get; set; } // "Regular Season", "Playoffs", "All"
        public int? SelectedSeasonId { get; set; }
        
        
        public int PageSize { get; set; } = 20;
        public int CurrentPage { get; set; } = 1;
        
        
        public bool ShowCalendarView { get; set; } = true;
        public bool ShowListView { get; set; } = false;
        
        
        public string CalendarTitle => $"{GetMonthName(SelectedMonth)} {SelectedYear}";
        public DateTime SelectedDate => new DateTime(SelectedYear, SelectedMonth, 1);
        public DateTime CalendarStartDate => SelectedDate.AddDays(-(int)SelectedDate.DayOfWeek);
        public DateTime CalendarEndDate => CalendarStartDate.AddDays(41); // 6 weeks
        
        
        public DateTime PreviousMonth => SelectedDate.AddMonths(-1);
        public DateTime NextMonth => SelectedDate.AddMonths(1);
        
        
        public List<int> AvailableYears { get; set; } = new List<int>();
        public List<string> AvailableSeasons { get; set; } = new List<string>();
        
        
        public List<GetGamesByTeamIdDTO.GameDto> GetGamesForDate(DateTime date)
        {
            return GamesData.Games
                .Where(g => g.GameDate.Date == date.Date)
                .OrderBy(g => g.GameDate)
                .ToList();
        }
        
        public bool HasGamesOnDate(DateTime date)
        {
            return GamesData.Games.Any(g => g.GameDate.Date == date.Date);
        }
        
        public int GetGamesCountForMonth()
        {
            var monthStart = new DateTime(SelectedYear, SelectedMonth, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);
            
            return GamesData.Games.Count(g => 
                g.GameDate.Date >= monthStart.Date && 
                g.GameDate.Date <= monthEnd.Date);
        }
        
        
        public List<List<DateTime>> GetCalendarWeeks()
        {
            var weeks = new List<List<DateTime>>();
            var currentDate = CalendarStartDate;
            
            for (int week = 0; week < 6; week++)
            {
                var weekDays = new List<DateTime>();
                for (int day = 0; day < 7; day++)
                {
                    weekDays.Add(currentDate);
                    currentDate = currentDate.AddDays(1);
                }
                weeks.Add(weekDays);
                
                
                if (currentDate > SelectedDate.AddMonths(1).AddDays(-1) && week >= 3)
                    break;
            }
            
            return weeks;
        }
        
        
        private string GetMonthName(int month)
        {
            return month switch
            {
                1 => "January",
                2 => "February", 
                3 => "March",
                4 => "April",
                5 => "May",
                6 => "June",
                7 => "July",
                8 => "August",
                9 => "September",
                10 => "October",
                11 => "November",
                12 => "December",
                _ => "Unknown"
            };
        }
        
        
        public string GetCurrentSeasonDisplay()
        {
            var currentDate = DateTime.Now;
            var currentYear = currentDate.Year;
            var currentMonth = currentDate.Month;
            
            
            if (currentMonth >= 10) // October-December
            {
                return $"{currentYear}-{currentYear + 1}";
            }
            else if (currentMonth <= 4) // January-April
            {
                return $"{currentYear - 1}-{currentYear}";
            }
            else // Off-season (May-September)
            {
                return $"{currentYear}-{currentYear + 1}";
            }
        }
        
        
        public void SetDefaults()
        {
            SelectedYear = DateTime.Now.Year;
            SelectedMonth = DateTime.Now.Month;
            SeasonType = "All";
            PageSize = 20;
            CurrentPage = 1;
            ShowCalendarView = true;
            ShowListView = false;
            
            
            var currentYear = DateTime.Now.Year;
            AvailableYears = Enumerable.Range(currentYear - 5, 7).ToList();
            
            
            AvailableSeasons = new List<string> { "All", "Regular Season", "Playoffs", "Preseason" };
        }
    }
}
