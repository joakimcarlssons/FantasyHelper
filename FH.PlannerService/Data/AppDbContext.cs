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

        #endregion
    }
}
