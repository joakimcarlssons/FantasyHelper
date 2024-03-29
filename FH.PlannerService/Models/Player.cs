﻿using System.ComponentModel.DataAnnotations.Schema;

namespace FH.PlannerService.Models
{
    public class Player
    {
        [Key]
        public int InternalPlayerId { get; set; }
        public int FantasyId { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public decimal Price { get; set; }
        public int Position { get; set; }
        public int? ChanceOfPlayingThisRound { get; set; }
        public int? ChanceOfPlayingNextRound { get; set; }

        public Team Team { get; set; }
    }
}
