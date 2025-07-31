using System;
using System.Collections.Generic;
using DapperKaggleProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperKaggleProject.Data;

public partial class NbadbContext : DbContext
{
    public NbadbContext()
    {
    }

    public NbadbContext(DbContextOptions<NbadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CommonPlayerInfo> CommonPlayerInfos { get; set; }

    public virtual DbSet<DraftCombineStat> DraftCombineStats { get; set; }

    public virtual DbSet<DraftHistory> DraftHistories { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameInfo> GameInfos { get; set; }

    public virtual DbSet<GameSummary> GameSummaries { get; set; }

    public virtual DbSet<InactivePlayer> InactivePlayers { get; set; }

    public virtual DbSet<LineScore> LineScores { get; set; }

    public virtual DbSet<PlayByPlayFull> PlayByPlayFulls { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamDetail> TeamDetails { get; set; }

    public virtual DbSet<TeamHistory> TeamHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommonPlayerInfo>(entity =>
        {
            entity.HasKey(e => e.PersonId);

            entity.ToTable("common_player_info");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("person_id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Country)
                .HasMaxLength(30)
                .HasColumnName("country");
            entity.Property(e => e.DisplayFiLast)
                .HasMaxLength(25)
                .HasColumnName("display_fi_last");
            entity.Property(e => e.DisplayFirstLast)
                .HasMaxLength(30)
                .HasColumnName("display_first_last");
            entity.Property(e => e.DisplayLastCommaFirst)
                .HasMaxLength(35)
                .HasColumnName("display_last_comma_first");
            entity.Property(e => e.DleagueFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("dleague_flag");
            entity.Property(e => e.DraftNumber)
                .HasMaxLength(15)
                .HasColumnName("draft_number");
            entity.Property(e => e.DraftRound)
                .HasMaxLength(15)
                .HasColumnName("draft_round");
            entity.Property(e => e.DraftYear)
                .HasMaxLength(15)
                .HasColumnName("draft_year");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name");
            entity.Property(e => e.FromYear).HasColumnName("from_year");
            entity.Property(e => e.GamesPlayedCurrentSeasonFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("games_played_current_season_flag");
            entity.Property(e => e.GamesPlayedFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("games_played_flag");
            entity.Property(e => e.Height)
                .HasMaxLength(10)
                .HasColumnName("height");
            entity.Property(e => e.Jersey)
                .HasMaxLength(10)
                .HasColumnName("jersey");
            entity.Property(e => e.LastAffiliation)
                .HasMaxLength(50)
                .HasColumnName("last_affiliation");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .HasColumnName("last_name");
            entity.Property(e => e.NbaFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nba_flag");
            entity.Property(e => e.PlayerSlug)
                .HasMaxLength(30)
                .HasColumnName("player_slug");
            entity.Property(e => e.Playercode)
                .HasMaxLength(40)
                .HasColumnName("playercode");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .HasColumnName("position");
            entity.Property(e => e.Rosterstatus)
                .HasMaxLength(10)
                .HasColumnName("rosterstatus");
            entity.Property(e => e.School)
                .HasMaxLength(50)
                .HasColumnName("school");
            entity.Property(e => e.SeasonExp).HasColumnName("season_exp");
            entity.Property(e => e.TeamAbbreviation)
                .HasMaxLength(5)
                .HasColumnName("team_abbreviation");
            entity.Property(e => e.TeamCity)
                .HasMaxLength(30)
                .HasColumnName("team_city");
            entity.Property(e => e.TeamCode)
                .HasMaxLength(15)
                .HasColumnName("team_code");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TeamName)
                .HasMaxLength(20)
                .HasColumnName("team_name");
            entity.Property(e => e.ToYear).HasColumnName("to_year");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Person).WithOne(p => p.CommonPlayerInfo)
                .HasForeignKey<CommonPlayerInfo>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_common_player_info_player");
        });

        modelBuilder.Entity<DraftCombineStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("draft_combine_stats");

            entity.Property(e => e.BenchPress).HasColumnName("bench_press");
            entity.Property(e => e.BodyFatPct)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("body_fat_pct");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.HandLength)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("hand_length");
            entity.Property(e => e.HandWidth)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("hand_width");
            entity.Property(e => e.HeightWShoes)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("height_w_shoes");
            entity.Property(e => e.HeightWShoesFtIn)
                .HasMaxLength(10)
                .HasColumnName("height_w_shoes_ft_in");
            entity.Property(e => e.HeightWoShoes)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("height_wo_shoes");
            entity.Property(e => e.HeightWoShoesFtIn)
                .HasMaxLength(10)
                .HasColumnName("height_wo_shoes_ft_in");
            entity.Property(e => e.LaneAgilityTime)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("lane_agility_time");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MaxVerticalLeap)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("max_vertical_leap");
            entity.Property(e => e.ModifiedLaneAgilityTime)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("modified_lane_agility_time");
            entity.Property(e => e.OffDribCollegeBreakLeft)
                .HasMaxLength(10)
                .HasColumnName("off_drib_college_break_left");
            entity.Property(e => e.OffDribCollegeBreakRight)
                .HasMaxLength(10)
                .HasColumnName("off_drib_college_break_right");
            entity.Property(e => e.OffDribCollegeTopKey)
                .HasMaxLength(10)
                .HasColumnName("off_drib_college_top_key");
            entity.Property(e => e.OffDribFifteenBreakLeft)
                .HasMaxLength(10)
                .HasColumnName("off_drib_fifteen_break_left");
            entity.Property(e => e.OffDribFifteenBreakRight)
                .HasMaxLength(10)
                .HasColumnName("off_drib_fifteen_break_right");
            entity.Property(e => e.OffDribFifteenTopKey)
                .HasMaxLength(10)
                .HasColumnName("off_drib_fifteen_top_key");
            entity.Property(e => e.OnMoveCollege)
                .HasMaxLength(10)
                .HasColumnName("on_move_college");
            entity.Property(e => e.OnMoveFifteen)
                .HasMaxLength(10)
                .HasColumnName("on_move_fifteen");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.PlayerName)
                .HasMaxLength(100)
                .HasColumnName("player_name");
            entity.Property(e => e.Position)
                .HasMaxLength(10)
                .HasColumnName("position");
            entity.Property(e => e.Season).HasColumnName("season");
            entity.Property(e => e.SpotCollegeBreakLeft)
                .HasMaxLength(10)
                .HasColumnName("spot_college_break_left");
            entity.Property(e => e.SpotCollegeBreakRight)
                .HasMaxLength(10)
                .HasColumnName("spot_college_break_right");
            entity.Property(e => e.SpotCollegeCornerLeft)
                .HasMaxLength(10)
                .HasColumnName("spot_college_corner_left");
            entity.Property(e => e.SpotCollegeCornerRight)
                .HasMaxLength(10)
                .HasColumnName("spot_college_corner_right");
            entity.Property(e => e.SpotCollegeTopKey)
                .HasMaxLength(10)
                .HasColumnName("spot_college_top_key");
            entity.Property(e => e.SpotFifteenBreakLeft)
                .HasMaxLength(10)
                .HasColumnName("spot_fifteen_break_left");
            entity.Property(e => e.SpotFifteenBreakRight)
                .HasMaxLength(10)
                .HasColumnName("spot_fifteen_break_right");
            entity.Property(e => e.SpotFifteenCornerLeft)
                .HasMaxLength(10)
                .HasColumnName("spot_fifteen_corner_left");
            entity.Property(e => e.SpotFifteenCornerRight)
                .HasMaxLength(10)
                .HasColumnName("spot_fifteen_corner_right");
            entity.Property(e => e.SpotFifteenTopKey)
                .HasMaxLength(10)
                .HasColumnName("spot_fifteen_top_key");
            entity.Property(e => e.SpotNbaBreakLeft)
                .HasMaxLength(10)
                .HasColumnName("spot_nba_break_left");
            entity.Property(e => e.SpotNbaBreakRight)
                .HasMaxLength(10)
                .HasColumnName("spot_nba_break_right");
            entity.Property(e => e.SpotNbaCornerLeft)
                .HasMaxLength(10)
                .HasColumnName("spot_nba_corner_left");
            entity.Property(e => e.SpotNbaCornerRight)
                .HasMaxLength(10)
                .HasColumnName("spot_nba_corner_right");
            entity.Property(e => e.SpotNbaTopKey)
                .HasMaxLength(10)
                .HasColumnName("spot_nba_top_key");
            entity.Property(e => e.StandingReach)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("standing_reach");
            entity.Property(e => e.StandingReachFtIn)
                .HasMaxLength(10)
                .HasColumnName("standing_reach_ft_in");
            entity.Property(e => e.StandingVerticalLeap)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("standing_vertical_leap");
            entity.Property(e => e.ThreeQuarterSprint)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("three_quarter_sprint");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("weight");
            entity.Property(e => e.Wingspan)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("wingspan");
            entity.Property(e => e.WingspanFtIn)
                .HasMaxLength(10)
                .HasColumnName("wingspan_ft_in");

            entity.HasOne(d => d.Player).WithMany()
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK_draft_combine_stats_player");
        });

        modelBuilder.Entity<DraftHistory>(entity =>
        {
            entity.HasKey(e => new { e.PersonId, e.Season });

            entity.ToTable("draft_history");

            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.Season).HasColumnName("season");
            entity.Property(e => e.DraftType)
                .HasMaxLength(20)
                .HasColumnName("draft_type");
            entity.Property(e => e.Organization)
                .HasMaxLength(100)
                .HasColumnName("organization");
            entity.Property(e => e.OrganizationType)
                .HasMaxLength(30)
                .HasColumnName("organization_type");
            entity.Property(e => e.OverallPick).HasColumnName("overall_pick");
            entity.Property(e => e.PlayerName)
                .HasMaxLength(100)
                .HasColumnName("player_name");
            entity.Property(e => e.PlayerProfileFlag).HasColumnName("player_profile_flag");
            entity.Property(e => e.RoundNumber).HasColumnName("round_number");
            entity.Property(e => e.RoundPick).HasColumnName("round_pick");
            entity.Property(e => e.TeamAbbreviation)
                .HasMaxLength(10)
                .HasColumnName("team_abbreviation");
            entity.Property(e => e.TeamCity)
                .HasMaxLength(50)
                .HasColumnName("team_city");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .HasColumnName("team_name");

            entity.HasOne(d => d.Team).WithMany(p => p.DraftHistories)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_draft_history_team");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("game");

            entity.Property(e => e.GameId)
                .HasMaxLength(10)
                .HasColumnName("game_id");
            entity.Property(e => e.AstAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("ast_away");
            entity.Property(e => e.AstHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("ast_home");
            entity.Property(e => e.BlkAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("blk_away");
            entity.Property(e => e.BlkHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("blk_home");
            entity.Property(e => e.DrebAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("dreb_away");
            entity.Property(e => e.DrebHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("dreb_home");
            entity.Property(e => e.Fg3PctAway)
                .HasColumnType("decimal(5, 3)")
                .HasColumnName("fg3_pct_away");
            entity.Property(e => e.Fg3PctHome)
                .HasColumnType("decimal(5, 3)")
                .HasColumnName("fg3_pct_home");
            entity.Property(e => e.Fg3aAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("fg3a_away");
            entity.Property(e => e.Fg3aHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("fg3a_home");
            entity.Property(e => e.Fg3mAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("fg3m_away");
            entity.Property(e => e.Fg3mHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("fg3m_home");
            entity.Property(e => e.FgPctAway)
                .HasColumnType("decimal(5, 3)")
                .HasColumnName("fg_pct_away");
            entity.Property(e => e.FgPctHome)
                .HasColumnType("decimal(5, 3)")
                .HasColumnName("fg_pct_home");
            entity.Property(e => e.FgaAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("fga_away");
            entity.Property(e => e.FgaHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("fga_home");
            entity.Property(e => e.FgmAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("fgm_away");
            entity.Property(e => e.FgmHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("fgm_home");
            entity.Property(e => e.FtPctAway)
                .HasColumnType("decimal(5, 3)")
                .HasColumnName("ft_pct_away");
            entity.Property(e => e.FtPctHome)
                .HasColumnType("decimal(5, 3)")
                .HasColumnName("ft_pct_home");
            entity.Property(e => e.FtaAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("fta_away");
            entity.Property(e => e.FtaHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("fta_home");
            entity.Property(e => e.FtmAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("ftm_away");
            entity.Property(e => e.FtmHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("ftm_home");
            entity.Property(e => e.GameDate).HasColumnName("game_date");
            entity.Property(e => e.MatchupAway)
                .HasMaxLength(20)
                .HasColumnName("matchup_away");
            entity.Property(e => e.MatchupHome)
                .HasMaxLength(20)
                .HasColumnName("matchup_home");
            entity.Property(e => e.Min).HasColumnName("min");
            entity.Property(e => e.OrebAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("oreb_away");
            entity.Property(e => e.OrebHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("oreb_home");
            entity.Property(e => e.PfAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("pf_away");
            entity.Property(e => e.PfHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("pf_home");
            entity.Property(e => e.PlusMinusAway).HasColumnName("plus_minus_away");
            entity.Property(e => e.PlusMinusHome).HasColumnName("plus_minus_home");
            entity.Property(e => e.PtsAway)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("pts_away");
            entity.Property(e => e.PtsHome)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("pts_home");
            entity.Property(e => e.RebAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("reb_away");
            entity.Property(e => e.RebHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("reb_home");
            entity.Property(e => e.SeasonId).HasColumnName("season_id");
            entity.Property(e => e.SeasonType)
                .HasMaxLength(20)
                .HasColumnName("season_type");
            entity.Property(e => e.StlAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("stl_away");
            entity.Property(e => e.StlHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("stl_home");
            entity.Property(e => e.TeamAbbreviationAway)
                .HasMaxLength(10)
                .HasColumnName("team_abbreviation_away");
            entity.Property(e => e.TeamAbbreviationHome)
                .HasMaxLength(10)
                .HasColumnName("team_abbreviation_home");
            entity.Property(e => e.TeamIdAway).HasColumnName("team_id_away");
            entity.Property(e => e.TeamIdHome).HasColumnName("team_id_home");
            entity.Property(e => e.TeamNameAway)
                .HasMaxLength(50)
                .HasColumnName("team_name_away");
            entity.Property(e => e.TeamNameHome)
                .HasMaxLength(50)
                .HasColumnName("team_name_home");
            entity.Property(e => e.TovAway)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("tov_away");
            entity.Property(e => e.TovHome)
                .HasColumnType("decimal(4, 1)")
                .HasColumnName("tov_home");
            entity.Property(e => e.VideoAvailableAway).HasColumnName("video_available_away");
            entity.Property(e => e.VideoAvailableHome).HasColumnName("video_available_home");
            entity.Property(e => e.WlAway)
                .HasMaxLength(2)
                .HasColumnName("wl_away");
            entity.Property(e => e.WlHome)
                .HasMaxLength(2)
                .HasColumnName("wl_home");

            entity.HasOne(d => d.TeamIdAwayNavigation).WithMany(p => p.GameTeamIdAwayNavigations)
                .HasForeignKey(d => d.TeamIdAway)
                .HasConstraintName("FK_game_team_away");

            entity.HasOne(d => d.TeamIdHomeNavigation).WithMany(p => p.GameTeamIdHomeNavigations)
                .HasForeignKey(d => d.TeamIdHome)
                .HasConstraintName("FK_game_team_home");
        });

        modelBuilder.Entity<GameInfo>(entity =>
        {
            entity.HasKey(e => e.GameId);

            entity.ToTable("game_info");

            entity.Property(e => e.GameId)
                .HasMaxLength(10)
                .HasColumnName("game_id");
            entity.Property(e => e.Attendance).HasColumnName("attendance");
            entity.Property(e => e.GameDate).HasColumnName("game_date");
            entity.Property(e => e.GameTime)
                .HasMaxLength(10)
                .HasColumnName("game_time");

            entity.HasOne(d => d.Game).WithOne(p => p.GameInfo)
                .HasForeignKey<GameInfo>(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_game_info_game");
        });

        modelBuilder.Entity<GameSummary>(entity =>
        {
            entity.HasKey(e => e.GameId);

            entity.ToTable("game_summary");

            entity.Property(e => e.GameId)
                .HasMaxLength(10)
                .HasColumnName("game_id");
            entity.Property(e => e.GameDateEst).HasColumnName("game_date_est");
            entity.Property(e => e.GameSequence).HasColumnName("game_sequence");
            entity.Property(e => e.GameStatusId).HasColumnName("game_status_id");
            entity.Property(e => e.GameStatusText)
                .HasMaxLength(20)
                .HasColumnName("game_status_text");
            entity.Property(e => e.Gamecode)
                .HasMaxLength(20)
                .HasColumnName("gamecode");
            entity.Property(e => e.HomeTeamId).HasColumnName("home_team_id");
            entity.Property(e => e.LivePcTime)
                .HasMaxLength(10)
                .HasColumnName("live_pc_time");
            entity.Property(e => e.LivePeriod).HasColumnName("live_period");
            entity.Property(e => e.LivePeriodTimeBcast)
                .HasMaxLength(30)
                .HasColumnName("live_period_time_bcast");
            entity.Property(e => e.NatlTvBroadcasterAbbreviation)
                .HasMaxLength(20)
                .HasColumnName("natl_tv_broadcaster_abbreviation");
            entity.Property(e => e.Season).HasColumnName("season");
            entity.Property(e => e.VisitorTeamId).HasColumnName("visitor_team_id");
            entity.Property(e => e.WhStatus).HasColumnName("wh_status");

            entity.HasOne(d => d.Game).WithOne(p => p.GameSummary)
                .HasForeignKey<GameSummary>(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_game_summary_game");
        });

        modelBuilder.Entity<InactivePlayer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("inactive_players");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.GameId)
                .HasMaxLength(10)
                .HasColumnName("game_id");
            entity.Property(e => e.JerseyNum).HasColumnName("jersey_num");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.TeamAbbreviation)
                .HasMaxLength(10)
                .HasColumnName("team_abbreviation");
            entity.Property(e => e.TeamCity)
                .HasMaxLength(50)
                .HasColumnName("team_city");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .HasColumnName("team_name");

            entity.HasOne(d => d.Game).WithMany()
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK_inactive_players_game");

            entity.HasOne(d => d.Player).WithMany()
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK_inactive_players_player");

            entity.HasOne(d => d.Team).WithMany()
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_inactive_players_team");
        });

        modelBuilder.Entity<LineScore>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("line_score");

            entity.Property(e => e.GameDateEst).HasColumnName("game_date_est");
            entity.Property(e => e.GameId)
                .HasMaxLength(10)
                .HasColumnName("game_id");
            entity.Property(e => e.GameSequence).HasColumnName("game_sequence");
            entity.Property(e => e.PtsAway).HasColumnName("pts_away");
            entity.Property(e => e.PtsHome).HasColumnName("pts_home");
            entity.Property(e => e.PtsOt10Away).HasColumnName("pts_ot10_away");
            entity.Property(e => e.PtsOt10Home).HasColumnName("pts_ot10_home");
            entity.Property(e => e.PtsOt1Away).HasColumnName("pts_ot1_away");
            entity.Property(e => e.PtsOt1Home).HasColumnName("pts_ot1_home");
            entity.Property(e => e.PtsOt2Away).HasColumnName("pts_ot2_away");
            entity.Property(e => e.PtsOt2Home).HasColumnName("pts_ot2_home");
            entity.Property(e => e.PtsOt3Away).HasColumnName("pts_ot3_away");
            entity.Property(e => e.PtsOt3Home).HasColumnName("pts_ot3_home");
            entity.Property(e => e.PtsOt4Away).HasColumnName("pts_ot4_away");
            entity.Property(e => e.PtsOt4Home).HasColumnName("pts_ot4_home");
            entity.Property(e => e.PtsOt5Away).HasColumnName("pts_ot5_away");
            entity.Property(e => e.PtsOt5Home).HasColumnName("pts_ot5_home");
            entity.Property(e => e.PtsOt6Away).HasColumnName("pts_ot6_away");
            entity.Property(e => e.PtsOt6Home).HasColumnName("pts_ot6_home");
            entity.Property(e => e.PtsOt7Away).HasColumnName("pts_ot7_away");
            entity.Property(e => e.PtsOt7Home).HasColumnName("pts_ot7_home");
            entity.Property(e => e.PtsOt8Away).HasColumnName("pts_ot8_away");
            entity.Property(e => e.PtsOt8Home).HasColumnName("pts_ot8_home");
            entity.Property(e => e.PtsOt9Away).HasColumnName("pts_ot9_away");
            entity.Property(e => e.PtsOt9Home).HasColumnName("pts_ot9_home");
            entity.Property(e => e.PtsQtr1Away).HasColumnName("pts_qtr1_away");
            entity.Property(e => e.PtsQtr1Home).HasColumnName("pts_qtr1_home");
            entity.Property(e => e.PtsQtr2Away).HasColumnName("pts_qtr2_away");
            entity.Property(e => e.PtsQtr2Home).HasColumnName("pts_qtr2_home");
            entity.Property(e => e.PtsQtr3Away).HasColumnName("pts_qtr3_away");
            entity.Property(e => e.PtsQtr3Home).HasColumnName("pts_qtr3_home");
            entity.Property(e => e.PtsQtr4Away).HasColumnName("pts_qtr4_away");
            entity.Property(e => e.PtsQtr4Home).HasColumnName("pts_qtr4_home");
            entity.Property(e => e.TeamAbbreviationAway)
                .HasMaxLength(10)
                .HasColumnName("team_abbreviation_away");
            entity.Property(e => e.TeamAbbreviationHome)
                .HasMaxLength(10)
                .HasColumnName("team_abbreviation_home");
            entity.Property(e => e.TeamCityNameAway)
                .HasMaxLength(50)
                .HasColumnName("team_city_name_away");
            entity.Property(e => e.TeamCityNameHome)
                .HasMaxLength(50)
                .HasColumnName("team_city_name_home");
            entity.Property(e => e.TeamIdAway).HasColumnName("team_id_away");
            entity.Property(e => e.TeamIdHome).HasColumnName("team_id_home");
            entity.Property(e => e.TeamNicknameAway)
                .HasMaxLength(50)
                .HasColumnName("team_nickname_away");
            entity.Property(e => e.TeamNicknameHome)
                .HasMaxLength(50)
                .HasColumnName("team_nickname_home");
            entity.Property(e => e.TeamWinsLossesAway)
                .HasMaxLength(20)
                .HasColumnName("team_wins_losses_away");
            entity.Property(e => e.TeamWinsLossesHome)
                .HasMaxLength(20)
                .HasColumnName("team_wins_losses_home");

            entity.HasOne(d => d.Game).WithMany()
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK_line_score_game");

            entity.HasOne(d => d.TeamIdAwayNavigation).WithMany()
                .HasForeignKey(d => d.TeamIdAway)
                .HasConstraintName("FK_line_score_team_away");

            entity.HasOne(d => d.TeamIdHomeNavigation).WithMany()
                .HasForeignKey(d => d.TeamIdHome)
                .HasConstraintName("FK_line_score_team_home");
        });

        modelBuilder.Entity<PlayByPlayFull>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("play_by_play_full");

            entity.Property(e => e.Eventmsgactiontype).HasColumnName("eventmsgactiontype");
            entity.Property(e => e.Eventmsgtype).HasColumnName("eventmsgtype");
            entity.Property(e => e.Eventnum).HasColumnName("eventnum");
            entity.Property(e => e.GameId)
                .HasMaxLength(10)
                .HasColumnName("game_id");
            entity.Property(e => e.Homedescription)
                .HasMaxLength(150)
                .HasColumnName("homedescription");
            entity.Property(e => e.Neutraldescription)
                .HasMaxLength(150)
                .HasColumnName("neutraldescription");
            entity.Property(e => e.Pctimestring)
                .HasMaxLength(10)
                .HasColumnName("pctimestring");
            entity.Property(e => e.Period).HasColumnName("period");
            entity.Property(e => e.Person1type).HasColumnName("person1type");
            entity.Property(e => e.Person2type).HasColumnName("person2type");
            entity.Property(e => e.Person3type).HasColumnName("person3type");
            entity.Property(e => e.Player1Id).HasColumnName("player1_id");
            entity.Property(e => e.Player1Name)
                .HasMaxLength(50)
                .HasColumnName("player1_name");
            entity.Property(e => e.Player1TeamAbbreviation)
                .HasMaxLength(10)
                .HasColumnName("player1_team_abbreviation");
            entity.Property(e => e.Player1TeamCity)
                .HasMaxLength(50)
                .HasColumnName("player1_team_city");
            entity.Property(e => e.Player1TeamId)
                .HasMaxLength(15)
                .HasColumnName("player1_team_id");
            entity.Property(e => e.Player1TeamNickname)
                .HasMaxLength(50)
                .HasColumnName("player1_team_nickname");
            entity.Property(e => e.Player2Id).HasColumnName("player2_id");
            entity.Property(e => e.Player2Name)
                .HasMaxLength(50)
                .HasColumnName("player2_name");
            entity.Property(e => e.Player2TeamAbbreviation)
                .HasMaxLength(10)
                .HasColumnName("player2_team_abbreviation");
            entity.Property(e => e.Player2TeamCity)
                .HasMaxLength(50)
                .HasColumnName("player2_team_city");
            entity.Property(e => e.Player2TeamId)
                .HasMaxLength(15)
                .HasColumnName("player2_team_id");
            entity.Property(e => e.Player2TeamNickname)
                .HasMaxLength(50)
                .HasColumnName("player2_team_nickname");
            entity.Property(e => e.Player3Id).HasColumnName("player3_id");
            entity.Property(e => e.Player3Name)
                .HasMaxLength(50)
                .HasColumnName("player3_name");
            entity.Property(e => e.Player3TeamAbbreviation)
                .HasMaxLength(10)
                .HasColumnName("player3_team_abbreviation");
            entity.Property(e => e.Player3TeamCity)
                .HasMaxLength(50)
                .HasColumnName("player3_team_city");
            entity.Property(e => e.Player3TeamId)
                .HasMaxLength(15)
                .HasColumnName("player3_team_id");
            entity.Property(e => e.Player3TeamNickname)
                .HasMaxLength(50)
                .HasColumnName("player3_team_nickname");
            entity.Property(e => e.Score)
                .HasMaxLength(15)
                .HasColumnName("score");
            entity.Property(e => e.Scoremargin)
                .HasMaxLength(10)
                .HasColumnName("scoremargin");
            entity.Property(e => e.VideoAvailableFlag).HasColumnName("video_available_flag");
            entity.Property(e => e.Visitordescription)
                .HasMaxLength(150)
                .HasColumnName("visitordescription");
            entity.Property(e => e.Wctimestring)
                .HasMaxLength(15)
                .HasColumnName("wctimestring");

            entity.HasOne(d => d.Game).WithMany()
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_play_by_play_game");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("player");

            entity.HasIndex(e => e.FullName, "IX_player_full_name");

            entity.HasIndex(e => e.IsActive, "IX_player_is_active");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name");
            entity.Property(e => e.FullName)
                .HasMaxLength(30)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("team");

            entity.HasIndex(e => e.Abbreviation, "IX_team_abbreviation");

            entity.HasIndex(e => e.City, "IX_team_city");

            entity.HasIndex(e => e.State, "IX_team_state");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(5)
                .HasColumnName("abbreviation");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasColumnName("city");
            entity.Property(e => e.FullName)
                .HasMaxLength(30)
                .HasColumnName("full_name");
            entity.Property(e => e.Nickname)
                .HasMaxLength(20)
                .HasColumnName("nickname");
            entity.Property(e => e.State)
                .HasMaxLength(25)
                .HasColumnName("state");
            entity.Property(e => e.YearFounded).HasColumnName("year_founded");
        });

        modelBuilder.Entity<TeamDetail>(entity =>
        {
            entity.HasKey(e => e.TeamId);

            entity.ToTable("team_details");

            entity.HasIndex(e => e.Abbreviation, "IX_team_details_abbreviation");

            entity.HasIndex(e => e.City, "IX_team_details_city");

            entity.HasIndex(e => e.Yearfounded, "IX_team_details_yearfounded");

            entity.Property(e => e.TeamId)
                .ValueGeneratedNever()
                .HasColumnName("team_id");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(5)
                .HasColumnName("abbreviation");
            entity.Property(e => e.Arena)
                .HasMaxLength(50)
                .HasColumnName("arena");
            entity.Property(e => e.Arenacapacity).HasColumnName("arenacapacity");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasColumnName("city");
            entity.Property(e => e.Dleagueaffiliation)
                .HasMaxLength(100)
                .HasColumnName("dleagueaffiliation");
            entity.Property(e => e.Facebook)
                .HasMaxLength(200)
                .HasColumnName("facebook");
            entity.Property(e => e.Generalmanager)
                .HasMaxLength(50)
                .HasColumnName("generalmanager");
            entity.Property(e => e.Headcoach)
                .HasMaxLength(50)
                .HasColumnName("headcoach");
            entity.Property(e => e.Instagram)
                .HasMaxLength(200)
                .HasColumnName("instagram");
            entity.Property(e => e.Nickname)
                .HasMaxLength(20)
                .HasColumnName("nickname");
            entity.Property(e => e.Owner)
                .HasMaxLength(100)
                .HasColumnName("owner");
            entity.Property(e => e.Twitter)
                .HasMaxLength(200)
                .HasColumnName("twitter");
            entity.Property(e => e.Yearfounded).HasColumnName("yearfounded");

            entity.HasOne(d => d.Team).WithOne(p => p.TeamDetail)
                .HasForeignKey<TeamDetail>(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_team_details_team");
        });

        modelBuilder.Entity<TeamHistory>(entity =>
        {
            entity.HasKey(e => new { e.TeamId, e.YearFounded });

            entity.ToTable("team_history");

            entity.HasIndex(e => e.City, "IX_team_history_city");

            entity.HasIndex(e => e.Nickname, "IX_team_history_nickname");

            entity.HasIndex(e => e.TeamId, "IX_team_history_team_id");

            entity.HasIndex(e => new { e.YearFounded, e.YearActiveTill }, "IX_team_history_years");

            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.YearFounded).HasColumnName("year_founded");
            entity.Property(e => e.City)
                .HasMaxLength(25)
                .HasColumnName("city");
            entity.Property(e => e.Nickname)
                .HasMaxLength(20)
                .HasColumnName("nickname");
            entity.Property(e => e.YearActiveTill).HasColumnName("year_active_till");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamHistories)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_team_history_team");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
