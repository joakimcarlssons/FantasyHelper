﻿namespace FH.FPL.FantasyDataProvider.Models
{
    /// <summary>
    /// 
    /// ONLY USED AS REFERENCE/DOCUMENTATION OF THE FPL API
    /// 
    /// </summary>

    public class Rootobject
    {
        public Event[] events { get; set; }
        public Game_Settings game_settings { get; set; }
        public Phase[] phases { get; set; }
        public Team[] teams { get; set; }
        public int total_players { get; set; }
        public Element[] elements { get; set; }
        public Element_Stats[] element_stats { get; set; }
        public Element_Types[] element_types { get; set; }
    }

    public class Game_Settings
    {
        public int league_join_private_max { get; set; }
        public int league_join_public_max { get; set; }
        public int league_max_size_public_classic { get; set; }
        public int league_max_size_public_h2h { get; set; }
        public int league_max_size_private_h2h { get; set; }
        public int league_max_ko_rounds_private_h2h { get; set; }
        public string league_prefix_public { get; set; }
        public int league_points_h2h_win { get; set; }
        public int league_points_h2h_lose { get; set; }
        public int league_points_h2h_draw { get; set; }
        public bool league_ko_first_instead_of_random { get; set; }
        public object cup_start_event_id { get; set; }
        public object cup_stop_event_id { get; set; }
        public object cup_qualifying_method { get; set; }
        public object cup_type { get; set; }
        public int squad_squadplay { get; set; }
        public int squad_squadsize { get; set; }
        public int squad_team_limit { get; set; }
        public int squad_total_spend { get; set; }
        public int ui_currency_multiplier { get; set; }
        public bool ui_use_special_shirts { get; set; }
        public object[] ui_special_shirt_exclusions { get; set; }
        public int stats_form_days { get; set; }
        public bool sys_vice_captain_enabled { get; set; }
        public int transfers_cap { get; set; }
        public float transfers_sell_on_fee { get; set; }
        public string[] league_h2h_tiebreak_stats { get; set; }
        public string timezone { get; set; }
    }

    public class Event
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime deadline_time { get; set; }
        public int average_entry_score { get; set; }
        public bool finished { get; set; }
        public bool data_checked { get; set; }
        public object highest_scoring_entry { get; set; }
        public int deadline_time_epoch { get; set; }
        public int deadline_time_game_offset { get; set; }
        public object highest_score { get; set; }
        public bool is_previous { get; set; }
        public bool is_current { get; set; }
        public bool is_next { get; set; }
        public bool cup_leagues_created { get; set; }
        public bool h2h_ko_matches_created { get; set; }
        public Chip_Plays[] chip_plays { get; set; }
        public int? most_selected { get; set; }
        public int? most_transferred_in { get; set; }
        public int? top_element { get; set; }
        public Top_Element_Info top_element_info { get; set; }
        public int transfers_made { get; set; }
        public int? most_captained { get; set; }
        public int? most_vice_captained { get; set; }
    }

    public class Top_Element_Info
    {
        public int id { get; set; }
        public int points { get; set; }
    }

    public class Chip_Plays
    {
        public string chip_name { get; set; }
        public int num_played { get; set; }
    }

    public class Phase
    {
        public int id { get; set; }
        public string name { get; set; }
        public int start_event { get; set; }
        public int stop_event { get; set; }
    }

    public class TeamBase
    {
        public int code { get; set; }
        public int draw { get; set; }
        public object form { get; set; }
        public int id { get; set; }
        public int loss { get; set; }
        public string name { get; set; }
        public int played { get; set; }
        public int points { get; set; }
        public int position { get; set; }
        public string short_name { get; set; }
        public int strength { get; set; }
        public object team_division { get; set; }
        public bool unavailable { get; set; }
        public int win { get; set; }
        public int strength_overall_home { get; set; }
        public int strength_overall_away { get; set; }
        public int strength_attack_home { get; set; }
        public int strength_attack_away { get; set; }
        public int strength_defence_home { get; set; }
        public int strength_defence_away { get; set; }
        public int pulse_id { get; set; }
    }

    public class Element
    {
        public int? chance_of_playing_next_round { get; set; }
        public object chance_of_playing_this_round { get; set; }
        public int code { get; set; }
        public int cost_change_event { get; set; }
        public int cost_change_event_fall { get; set; }
        public int cost_change_start { get; set; }
        public int cost_change_start_fall { get; set; }
        public int dreamteam_count { get; set; }
        public int element_type { get; set; }
        public string ep_next { get; set; }
        public object ep_this { get; set; }
        public int event_points { get; set; }
        public string first_name { get; set; }
        public string form { get; set; }
        public int id { get; set; }
        public bool in_dreamteam { get; set; }
        public string news { get; set; }
        public DateTime? news_added { get; set; }
        public int now_cost { get; set; }
        public string photo { get; set; }
        public string points_per_game { get; set; }
        public string second_name { get; set; }
        public string selected_by_percent { get; set; }
        public bool special { get; set; }
        public object squad_number { get; set; }
        public string status { get; set; }
        public int team { get; set; }
        public int team_code { get; set; }
        public int total_points { get; set; }
        public int transfers_in { get; set; }
        public int transfers_in_event { get; set; }
        public int transfers_out { get; set; }
        public int transfers_out_event { get; set; }
        public string value_form { get; set; }
        public string value_season { get; set; }
        public string web_name { get; set; }
        public int minutes { get; set; }
        public int goals_scored { get; set; }
        public int assists { get; set; }
        public int clean_sheets { get; set; }
        public int goals_conceded { get; set; }
        public int own_goals { get; set; }
        public int penalties_saved { get; set; }
        public int penalties_missed { get; set; }
        public int yellow_cards { get; set; }
        public int red_cards { get; set; }
        public int saves { get; set; }
        public int bonus { get; set; }
        public int bps { get; set; }
        public string influence { get; set; }
        public string creativity { get; set; }
        public string threat { get; set; }
        public string ict_index { get; set; }
        public int influence_rank { get; set; }
        public int influence_rank_type { get; set; }
        public int creativity_rank { get; set; }
        public int creativity_rank_type { get; set; }
        public int threat_rank { get; set; }
        public int threat_rank_type { get; set; }
        public int ict_index_rank { get; set; }
        public int ict_index_rank_type { get; set; }
        public int? corners_and_indirect_freekicks_order { get; set; }
        public string corners_and_indirect_freekicks_text { get; set; }
        public int? direct_freekicks_order { get; set; }
        public string direct_freekicks_text { get; set; }
        public int? penalties_order { get; set; }
        public string penalties_text { get; set; }
    }

    public class Element_Stats
    {
        public string label { get; set; }
        public string name { get; set; }
    }

    public class Element_Types
    {
        public int id { get; set; }
        public string plural_name { get; set; }
        public string plural_name_short { get; set; }
        public string singular_name { get; set; }
        public string singular_name_short { get; set; }
        public int squad_select { get; set; }
        public int squad_min_play { get; set; }
        public int squad_max_play { get; set; }
        public bool ui_shirt_specific { get; set; }
        public int?[] sub_positions_locked { get; set; }
        public int element_count { get; set; }
    }


    public class RootobjectFixtures
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public int code { get; set; }
        public int _event { get; set; }
        public bool finished { get; set; }
        public bool finished_provisional { get; set; }
        public int id { get; set; }
        public DateTime kickoff_time { get; set; }
        public int minutes { get; set; }
        public bool provisional_start_time { get; set; }
        public bool started { get; set; }
        public int team_a { get; set; }
        public object team_a_score { get; set; }
        public int team_h { get; set; }
        public object team_h_score { get; set; }
        public object[] stats { get; set; }
        public int team_h_difficulty { get; set; }
        public int team_a_difficulty { get; set; }
        public int pulse_id { get; set; }
    }

}
