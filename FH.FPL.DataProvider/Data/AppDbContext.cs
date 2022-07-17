namespace FantasyHelper.FPL.DataProvider.Data
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
        public DbSet<Fixture> Fixtures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // A team can have many players
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId);

            // A team can have multiple home fixtures
            modelBuilder.Entity<Team>()
                .HasMany(t => t.HomeFixtures)
                .WithOne(f => f.HomeTeam)
                .HasForeignKey(f => f.HomeTeamId);

            // A team can have multiple away fixtures
            modelBuilder.Entity<Team>()
                .HasMany(t => t.AwayFixtures)
                .WithOne(f => f.AwayTeam)
                .HasForeignKey(f => f.AwayTeamId);

            // A player can only have one team
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            // A gameweek can have many fixtures
            modelBuilder.Entity<Gameweek>()
                .HasMany(gw => gw.Fixtures)
                .WithOne(f => f.Gameweek)
                .HasForeignKey(f => f.GameweekId);

            // A fixture can only belong to one gameweek
            modelBuilder.Entity<Fixture>()
                .HasOne(f => f.Gameweek)
                .WithMany(gw => gw.Fixtures)
                .HasForeignKey(f => f.GameweekId);

            // A fixture can only have one home team
            modelBuilder.Entity<Fixture>()
                .HasOne(f => f.HomeTeam)
                .WithMany(t => t.HomeFixtures)
                .HasForeignKey(f => f.HomeTeamId);

            // A fixture can only have one away team
            modelBuilder.Entity<Fixture>()
                .HasOne(f => f.AwayTeam)
                .WithMany(t => t.AwayFixtures)
                .HasForeignKey(f => f.AwayTeamId);
        }

        #endregion
    }
}
