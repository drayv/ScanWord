using WatchWord.Domain;

namespace WatchWord.Infrastructure
{
    /// <summary>The project settings.</summary>
    public class WatchProjectSettings : IWatchProjectSettings
    {
        /// <summary>Initializes a new instance of the <see cref="WatchProjectSettings"/> class.</summary>
        public WatchProjectSettings()
        {
            DataBaseName = "WatchWord";
        }

        /// <summary>Gets the database name.</summary>
        public string DataBaseName { get; private set; }
    }
}