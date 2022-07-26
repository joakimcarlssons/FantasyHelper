using Microsoft.EntityFrameworkCore;

namespace FH.PlannerService.Data
{
    public class AppDbContext : DbContext
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        #endregion

        #region DbSets

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // A player can only have one team
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasPrincipalKey(t => new { t.TeamId, t.FantasyId })
                .HasForeignKey(p => new { p.TeamId, p.FantasyId });

            // A fixture can only have one home team
            modelBuilder.Entity<Fixture>()
                .HasOne(f => f.HomeTeam)
                .WithMany(t => t.HomeFixtures)
                .HasPrincipalKey(t => new { t.TeamId, t.FantasyId })
                .HasForeignKey(f => new { f.HomeTeamId, f.FantasyId });

            // A fixture can only have one away team
            modelBuilder.Entity<Fixture>()
                .HasOne(f => f.AwayTeam)
                .WithMany(t => t.AwayFixtures)
                .HasPrincipalKey(t => new { t.TeamId, t.FantasyId })
                .HasForeignKey(f => new { f.AwayTeamId, f.FantasyId });
        }

        #endregion
    }
}
