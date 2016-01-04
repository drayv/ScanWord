using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IWatchWordUnitOfWork _watchWordUnitOfWork;

        /// <summary>Prevents a default instance of the <see cref="VocabularyService"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private AccountService()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VocabularyService"/> class.</summary>
        /// <param name="watchWordUnitOfWork">Unit of work over WatchWord repositories.</param>
        public AccountService(IWatchWordUnitOfWork watchWordUnitOfWork)
        {
            _watchWordUnitOfWork = watchWordUnitOfWork;
        }

        public Account GetByExternalId(int id)
        {
            var account = _watchWordUnitOfWork.AccountsRepository.GetByСondition(a => a.ExternalId == id);
            if (account != null) return account;
            var newAccount = new Account { ExternalId = id };
            _watchWordUnitOfWork.AccountsRepository.Insert(newAccount);
            _watchWordUnitOfWork.Commit();
            return newAccount;
        }
    }
}