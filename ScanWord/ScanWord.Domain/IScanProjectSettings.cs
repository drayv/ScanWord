namespace ScanWord.Domain
{
    /// <summary>
    /// The ProjectSettings interface.
    /// </summary>
    public interface IScanProjectSettings
    {
        /// <summary>
        /// Gets database name.
        /// </summary>
        string DataBaseName { get; }
    }
}