using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Concrete
{
    /// <summary>Represents a layer for work with user vocabularies.</summary>
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

        /// <summary>Gets all words from user vocabulary of known words.</summary>
        /// <param name="userId">External user identifier.</param>
        /// <returns>List of words.</returns>
        public async Task<List<KnownWord>> GetKnownWords(int userId)
        {
            var account = _accountService.GetByExternalId(userId);
            return await _watchWordUnitOfWork.KnownWordsRepository.GetAllAsync(k => k.Owner.Id == account.Id);
        }

        /// <summary>Gets all words from user vocabulary of learning words.</summary>
        /// <param name="userId">External user identifier.</param>
        /// <returns>List of words.</returns>
        public async Task<List<LearnWord>> GetLearnWords(int userId)
        {
            var account = _accountService.GetByExternalId(userId);
            return await _watchWordUnitOfWork.LearnWordsRepository.GetAllAsync(l => l.Owner.Id == account.Id);
        }

        /// <summary>Inserts word to the user vocabulary of known words.</summary>
        /// <param name="word">Original word.</param>
        /// <param name="translation">Translation of the word.</param>
        /// <param name="userId">External user identifier.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        public int InsertKnownWord(string word, string translation, int userId)
        {
            var account = _accountService.GetByExternalId(userId);
            var knownWord = new KnownWord { Word = word, Translation = translation, Owner = account, Type = VocabType.KnownWord };
            _watchWordUnitOfWork.KnownWordsRepository.Insert(knownWord);
            return _watchWordUnitOfWork.Commit();
        }

        /// <summary>Inserts word to the user vocabulary of learning words.</summary>
        /// <param name="word">Original word.</param>
        /// <param name="translation">Translation of the word.</param>
        /// <param name="userId">External user identifier.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        public int InsertLearnWord(string word, string translation, int userId)
        {
            var account = _accountService.GetByExternalId(userId);
            var learnWord = new LearnWord { Word = word, Translation = translation, Owner = account, Type = VocabType.LearnWord };
            _watchWordUnitOfWork.LearnWordsRepository.Insert(learnWord);
            return _watchWordUnitOfWork.Commit();
        }

        /// <summary>Fills list of words with translation.</summary>
        /// <param name="words">Words without translation.</param>
        /// <returns>List of words with translation.</returns>
        public async Task<List<VocabWord>> FillVocabByWords(string[] words)
        {
            var vocabWords = new List<VocabWord>();

            vocabWords.AddRange(await _watchWordUnitOfWork.LearnWordsRepository.GetAllAsync(l => words.Contains(l.Word)));
            vocabWords.AddRange(await _watchWordUnitOfWork.KnownWordsRepository.GetAllAsync(k => words.Contains(k.Word)));

            foreach (var word in words.Where(word => vocabWords.All(v => v.Word != word)))
            {
                vocabWords.Add(new VocabWord { Word = word, Translation = "no" });
            }

            return vocabWords;
        }
    }
}