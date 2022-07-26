using FH.FA.DataProvider.Models;

namespace FH.FA.DataProvider.Data
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

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Gameweek> Gameweeks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // A team can have many players
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId);

            // A player can only have one team
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);
        }

        #endregion
    }
}
