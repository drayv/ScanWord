namespace ScanWord.Domain
{
    /// <summary>
    /// The ProjectSettings interface.
    /// </summary>
    public interface IProjectSettings
    {
        /// <summary>
        /// Gets database name.
        /// </summary>
        string DataBaseName { get; }
    }
}