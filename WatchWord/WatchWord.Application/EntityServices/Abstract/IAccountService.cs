using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Abstract
{
    public interface IAccountService
    {
        Account GetByExternalId(int id);
    }
}