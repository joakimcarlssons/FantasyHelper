namespace FantasyHelper.FA.DataProvider.Data
{
    /// <summary>
    /// Handling the background service of periodically fetching and updating data from Fantasy Allsvenskan 
    /// </summary>
    public class PeriodicDataLoader : BackgroundService
    {
        #region Private Members

        private readonly IDataLoader _dataLoader;
        private readonly FAOptions _faConfig;

        private readonly PeriodicTimer _timer;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PeriodicDataLoader(IOptions<FAOptions> faConfig, IDataLoader dataLoader)
        {
            _faConfig = faConfig.Value;
            _dataLoader = dataLoader;
            _timer = new(TimeSpan.FromSeconds(_faConfig.LoadingInterval));
        }

        #endregion

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                Console.WriteLine("--> Data load of Fantasy Allsvenskan data started.");

                if (!(await _dataLoader.SaveToDatabase()))
                {
                    Console.WriteLine("--> Failed to save Fantasy Allsvenskan data to database!");
                    throw new Exception("Failed to save Fantasy Allsvenskan data to database!");
                }

                Console.WriteLine("--> Data load of Fantasy Allsvenskan data finished.");
            }
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested);
        }
    }
}
