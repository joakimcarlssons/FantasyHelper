namespace FantasyHelper.FPL.DataProvider.Dtos.External
{
    /// <summary>
    /// The player DTO from FPL Api
    /// </summary>
    public class ExternalPlayerDto
    {
        [JsonPropertyName("id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("second_name")]
        public string LastName { get; set; }

        [JsonPropertyName("web_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("now_cost")]
        public int Price { get; set; }

        [JsonPropertyName("team")]
        public int TeamId { get; set; }

        [JsonPropertyName("form")]
        public string Form { get; set; }

        [JsonPropertyName("element_type")]
        public int Position { get; set; }

        [JsonPropertyName("chance_of_playing_this_round")]
        public int? ChanceOfPlayingThisRound { get; set; }

        [JsonPropertyName("chance_of_playing_next_round")]
        public int? ChanceOfPlayingNextRound { get; set; }

        [JsonPropertyName("selected_by_percent")]
        public string SelectedByPercent { get; set; }

        [JsonPropertyName("yellow_cards")]
        public int YellowCards { get; set; }

        [JsonPropertyName("red_cards")]
        public int RedCards { get; set; }

        [JsonPropertyName("goals_scored")]
        public int Goals { get; set; }

        [JsonPropertyName("assists")]
        public int Assists { get; set; }

        [JsonPropertyName("clean_sheets")]
        public int CleanSheets { get; set; }

        [JsonPropertyName("goals_conceded")]
        public int GoalsConceded { get; set; }

        [JsonPropertyName("minutes")]
        public int MinutesPlayed { get; set; }

        [JsonPropertyName("saves")]
        public int Saves { get; set; }

        [JsonPropertyName("bonus")]
        public int Bonus { get; set; }

        [JsonPropertyName("bps")]
        public int Bps { get; set; }

        [JsonPropertyName("corners_and_indirect_freekicks_order")]
        public int? CornersOrder { get; set; }

        [JsonPropertyName("direct_freekicks_order")]
        public int? FreekicksOrder { get; set; }

        [JsonPropertyName("penalties_order")]
        public int? PenaltiesOrder { get; set; }
    }
}
