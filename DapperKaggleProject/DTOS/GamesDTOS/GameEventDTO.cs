namespace DapperKaggleProject.DTOS.GamesDTOS
{
    public class GameEventDTO
    {
        public string GameId { get; set; } = null!;
        public int EventNum { get; set; }
        public int EventMsgType { get; set; }
        public int EventMsgActionType { get; set; }
        public int Period { get; set; }
        public string? WcTimeString { get; set; }
        public string? PcTimeString { get; set; }
        public string? HomeDescription { get; set; }
        public string? NeutralDescription { get; set; }
        public string? VisitorDescription { get; set; }
        public string? Score { get; set; }
        public string? ScoreMargin { get; set; }
        
        
        public decimal? Person1Type { get; set; }
        public long? Player1Id { get; set; }
        public string? Player1Name { get; set; }
        public decimal? Player1TeamId { get; set; }
        public string? Player1TeamCity { get; set; }
        public string? Player1TeamNickname { get; set; }
        public string? Player1TeamAbbreviation { get; set; }
        
        
        public decimal? Person2Type { get; set; }
        public long? Player2Id { get; set; }
        public string? Player2Name { get; set; }
        public decimal? Player2TeamId { get; set; }
        public string? Player2TeamCity { get; set; }
        public string? Player2TeamNickname { get; set; }
        public string? Player2TeamAbbreviation { get; set; }
        
        
        public decimal? Person3Type { get; set; }
        public long? Player3Id { get; set; }
        public string? Player3Name { get; set; }
        public decimal? Player3TeamId { get; set; }
        public string? Player3TeamCity { get; set; }
        public string? Player3TeamNickname { get; set; }
        public string? Player3TeamAbbreviation { get; set; }
        
        public int VideoAvailableFlag { get; set; }

        
        public string EventDescription => GetEventDescription();
        public string EventTimeDisplay => $"Q{Period} {PcTimeString}";
        public bool IsScoring => EventMsgType == 1 || EventMsgType == 3; // Made shot or Free throw
        public bool IsMiss => EventMsgType == 2; // Missed shot
        public bool IsRebound => EventMsgType == 4; // Rebound
        public bool IsSubstitution => EventMsgType == 8; // Substitution
        public bool IsFoul => EventMsgType == 6; // Foul
        public bool IsTurnover => EventMsgType == 5; // Turnover
        public bool IsTimeout => EventMsgType == 9; // Timeout
        public string EventTypeIcon => GetEventTypeIcon();
        public string EventTypeClass => GetEventTypeClass();

        private string GetEventDescription()
        {
            
            if (!string.IsNullOrEmpty(HomeDescription))
                return HomeDescription;
            if (!string.IsNullOrEmpty(VisitorDescription))
                return VisitorDescription;
            if (!string.IsNullOrEmpty(NeutralDescription))
                return NeutralDescription;
            
            return "Game Event";
        }

        private string GetEventTypeIcon()
        {
            return EventMsgType switch
            {
                1 => "fas fa-basketball-ball", // Made shot
                2 => "fas fa-times-circle",   // Missed shot
                3 => "fas fa-bullseye",       // Free throw
                4 => "fas fa-hand-paper",     // Rebound
                5 => "fas fa-exchange-alt",   // Turnover
                6 => "fas fa-exclamation-triangle", // Foul
                8 => "fas fa-user-friends",   // Substitution
                9 => "fas fa-clock",          // Timeout
                10 => "fas fa-circle",        // Jump ball
                12 => "fas fa-play",          // Period start
                13 => "fas fa-stop",          // Period end
                _ => "fas fa-circle"          // Default
            };
        }

        private string GetEventTypeClass()
        {
            return EventMsgType switch
            {
                1 => "event-score",     // Made shot
                2 => "event-miss",      // Missed shot
                3 => "event-freethrow", // Free throw
                4 => "event-rebound",   // Rebound
                5 => "event-turnover",  // Turnover
                6 => "event-foul",      // Foul
                8 => "event-sub",       // Substitution
                9 => "event-timeout",   // Timeout
                10 => "event-jumpball", // Jump ball
                12 => "event-start",    // Period start
                13 => "event-end",      // Period end
                _ => "event-default"    // Default
            };
        }
    }
}
