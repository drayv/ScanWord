using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Abstract
{
    /// <summary>Represents a layer for work with user's accounts.</summary>
    public interface IAccountService
    {
        /// <summary>Gets user's account by external identifier.</summary>
        /// <param name="id">External identifier, asp.net identity id, etc.</param>
        /// <returns>User's account.</returns>
        Account GetByExternalId(int id);
    }
}