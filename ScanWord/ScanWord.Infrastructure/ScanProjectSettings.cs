using ScanWord.Domain;

namespace ScanWord.Infrastructure
{
    /// <summary>The project settings.</summary>
    public class ScanProjectSettings : IScanProjectSettings
    {
        /// <summary>Initializes a new instance of the <see cref="ScanProjectSettings"/> class.</summary>
        public ScanProjectSettings()
        {
            DataBaseName = "ScanWord";
        }

        /// <summary>Gets the database name.</summary>
        public string DataBaseName { get; private set; }
    }
}