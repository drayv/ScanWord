namespace WatchWord.Logic
{
    using WatchWord.Domain.Common;

    /// <summary>
    /// The project settings.
    /// </summary>
    public class ProjectSettings : IProjectSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSettings"/> class.
        /// </summary>
        public ProjectSettings()
        {
            this.DataBaseName = "WathWord";
        }

        /// <summary>
        /// Gets the database name.
        /// </summary>
        public string DataBaseName { get; private set; }
    }
}