using ScanWord.Core.Abstract;
using ScanWord.Core.Data;

namespace ScanWord.Core.Concrete
{
    /// <summary>Provides batch work with ScanWord data.</summary>
    public class ScanWordDataHandler : IScanWordDataHandler
    {
        /// <summary>Gets or sets the database repository.</summary>
        // ReSharper disable once NotAccessedField.Local
        private readonly IScanDataUnitOfWork _repositories;

        /// <summary>Initializes a new instance of the <see cref="ScanWordDataHandler"/> class.</summary>
        /// <param name="repositories">UnitOfWork for ScanWord data repositories.</param>
        public ScanWordDataHandler(IScanDataUnitOfWork repositories)
        {
            _repositories = repositories;
        }

        /// <summary>Prevents a default instance of the <see cref="ScanWordDataHandler"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private ScanWordDataHandler()
        {
        }
    }
}