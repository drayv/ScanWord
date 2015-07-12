using ScanWord.Domain.Common;
using ScanWord.Domain.Data;

namespace ScanWord.Service
{
    /// <summary>
    /// Provides batch work with ScanWord data.
    /// </summary>
    public class ScanWordDataHandler : IScanWordDataHandler
    {
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        // ReSharper disable once NotAccessedField.Local
        private readonly IScanDataRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanWordDataHandler"/> class.
        /// </summary>
        /// <param name="repository">ScanWord data repository.</param>
        public ScanWordDataHandler(IScanDataRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ScanWordDataHandler"/> class from being created.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private ScanWordDataHandler()
        {
        }
    }
}