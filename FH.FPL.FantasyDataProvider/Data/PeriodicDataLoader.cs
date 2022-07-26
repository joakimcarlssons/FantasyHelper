using Microsoft.Extensions.Options;

namespace FH.FPL.FantasyDataProvider.Data
{
    /// <summary>
    /// Handling the background service of periodically fetching and updating data from FPL
    /// </summary>
    public class PeriodicDataLoader : BackgroundService
    {
        #region Private Members

        private readonly IDataLoader _dataLoader;
        private readonly PeriodicTimer _timer;
        private readonly FPLOptions _fplConfig;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PeriodicDataLoader(IOptions<FPLOptions> fplConfig, IDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
            _fplConfig = fplConfig.Value;
            _timer = new(TimeSpan.FromMinutes(_fplConfig.LoadingInterval));
        }

        #endregion

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                Console.WriteLine("--> Data load of FPL data started.");

                if (!(await _dataLoader.SaveToDatabase()))
                {
                    Console.WriteLine("--> Failed to save FPL data to database!");
                    throw new Exception("Failed to save FPL data to database!");
                }

                Console.WriteLine("--> Data load of FPL data finished.");
            }
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested);
        }
    }
}
