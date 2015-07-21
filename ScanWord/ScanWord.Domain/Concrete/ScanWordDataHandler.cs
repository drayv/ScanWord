using ScanWord.Core.Abstract;
using ScanWord.Core.Data;

//TODO: change path from "ScanWord\ScanWord\ScanWord.Domain" to "ScanWord\ScanWord\ScanWord.Core" in project(!)
namespace ScanWord.Core.Concrete
{
    /// <summary>Provides batch work with ScanWord data.</summary>
    public class ScanWordDataHandler : IScanWordDataHandler
    {
        /// <summary>Gets or sets the database repository.</summary>
        // ReSharper disable once NotAccessedField.Local
        private readonly IScanDataRepository _repository;

        /// <summary>Initializes a new instance of the <see cref="ScanWordDataHandler"/> class.</summary>
        /// <param name="repository">ScanWord data repository.</param>
        public ScanWordDataHandler(IScanDataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>Prevents a default instance of the <see cref="ScanWordDataHandler"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private ScanWordDataHandler()
        {
        }
    }
}