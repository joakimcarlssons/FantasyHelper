namespace FH.FA.FixturesProvider.Data
{
    public class AppDbContext : DbContext
    {
        #region Constructor

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        #endregion

        #region DbSets

        public DbSet<Team> Teams { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // A team can have many home fixtures
            modelBuilder.Entity<Team>()
                .HasMany(t => t.HomeFixtures)
                .WithOne(f => f.HomeTeam)
                .HasForeignKey(f => f.HomeTeamId);

            // A team can have many away fixtures
            modelBuilder.Entity<Team>()
                .HasMany(t => t.AwayFixtures)
                .WithOne(f => f.AwayTeam)
                .HasForeignKey(f => f.AwayTeamId);

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
