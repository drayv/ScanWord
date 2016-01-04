using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Concrete
{
    public class VocabularyService : IVocabularyService
    {
        private readonly IWatchWordUnitOfWork _watchWordUnitOfWork;
        private readonly IAccountService _accountService;

        /// <summary>Prevents a default instance of the <see cref="VocabularyService"/> class from being created.</summary>
        // ReSharper disable once UnusedMember.Local
        private VocabularyService()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VocabularyService"/> class.</summary>
        /// <param name="watchWordUnitOfWork">Unit of work over WatchWord repositories.</param>
        public VocabularyService(IWatchWordUnitOfWork watchWordUnitOfWork)
        {
            _watchWordUnitOfWork = watchWordUnitOfWork;
            _accountService = new AccountService(watchWordUnitOfWork);
        }

        public async Task<List<KnownWord>> GetKnownWords(int userId)
        {
            var account = _accountService.GetByExternalId(userId);
            return await _watchWordUnitOfWork.KnownWordsRepository.GetAllAsync(k => k.Owner.Id == account.Id);
        }

        public async Task<List<LearnWord>> GetLearnWords(int userId)
        {
            var account = _accountService.GetByExternalId(userId);
            return await _watchWordUnitOfWork.LearnWordsRepository.GetAllAsync(l => l.Owner.Id == account.Id);
        }

        public int InsertKnownWord(string word, string translation, int userId)
        {
            var account = _accountService.GetByExternalId(userId);
            var knownWord = new KnownWord { Word = word, Translation = translation, Owner = account };
            _watchWordUnitOfWork.KnownWordsRepository.Insert(knownWord);
            return _watchWordUnitOfWork.Commit();
        }

        public int InsertLearnWord(string word, string translation, int userId)
        {
            var account = _accountService.GetByExternalId(userId);
            var learnWord = new LearnWord { Word = word, Translation = translation, Owner = account };
            _watchWordUnitOfWork.LearnWordsRepository.Insert(learnWord);
            return _watchWordUnitOfWork.Commit();
        }
    }
}