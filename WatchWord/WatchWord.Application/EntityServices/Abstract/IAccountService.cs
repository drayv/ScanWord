using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Abstract
{
    /// <summary>Represents a layer for work with user accounts.</summary>
    public interface IAccountService
    {
        /// <summary>Gets user account by external identifier.</summary>
        /// <param name="id">External identifier, asp.net identity id, e.t.c.</param>
        /// <returns>User account.</returns>
        Account GetByExternalId(int id);
    }
}