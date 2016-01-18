using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.DataAccess;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Concrete
{
    /// <summary>Represents a layer for work with user's vocabularies.</summary>
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

        /// <summary>Gets all words from user's vocabulary of known words.</summary>
        /// <param name="userId">External user's identifier.</param>
        /// <returns>List of words.</returns>
        public async Task<List<KnownWord>> GetKnownWords(int userId)
        {
            var account = _accountService.GetByExternalId(userId);
            return await _watchWordUnitOfWork.KnownWordsRepository.GetAllAsync(k => k.Owner.Id == account.Id);
        }

        /// <summary>Gets all words from user's vocabulary of learning words.</summary>
        /// <param name="userId">External user's identifier.</param>
        /// <returns>List of words.</returns>
        public async Task<List<LearnWord>> GetLearnWords(int userId)
        {
            var account = _accountService.GetByExternalId(userId);
            return await _watchWordUnitOfWork.LearnWordsRepository.GetAllAsync(l => l.Owner.Id == account.Id);
        }

        /// <summary>Inserts word to the user's vocabulary of known words.</summary>
        /// <param name="word">Original word.</param>
        /// <param name="translation">Translation of the word.</param>
        /// <param name="userId">External user's identifier.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        public int InsertKnownWord(string word, string translation, int userId)
        {
            var owner = _accountService.GetByExternalId(userId);
            var knownWord = new KnownWord { Word = word, Translation = translation, Owner = owner, Type = VocabType.KnownWord };

            var existing = _watchWordUnitOfWork.KnownWordsRepository.GetByСondition(
                k => k.Owner.Id == owner.Id && k.Word == word, k => k.Owner);

            if (existing != null)
            {
                // Update translation if word exist.
                existing.Translation = translation;
                _watchWordUnitOfWork.KnownWordsRepository.Update(existing);
            }
            else
            {
                // Insert if this a new word.
                _watchWordUnitOfWork.KnownWordsRepository.Insert(knownWord);
            }

            return _watchWordUnitOfWork.Commit();
        }

        /// <summary>Inserts word to the user's vocabulary of learning words.</summary>
        /// <param name="word">Original word.</param>
        /// <param name="translation">Translation of the word.</param>
        /// <param name="userId">External user's identifier.</param>
        /// <returns>The count of changed elements in data storage.</returns>
        public int InsertLearnWord(string word, string translation, int userId)
        {
            var owner = _accountService.GetByExternalId(userId);
            var learnWord = new LearnWord { Word = word, Translation = translation, Owner = owner, Type = VocabType.LearnWord };

            var existing = _watchWordUnitOfWork.LearnWordsRepository.GetByСondition(
                l => l.Owner.Id == owner.Id && l.Word == word, l => l.Owner);

            if (existing != null)
            {
                // Update translation if word exist.
                existing.Translation = translation;
                _watchWordUnitOfWork.LearnWordsRepository.Update(existing);
            }
            else
            {
                // Insert if this a new word.
                _watchWordUnitOfWork.LearnWordsRepository.Insert(learnWord);
            }

            return _watchWordUnitOfWork.Commit();
        }

        /// <summary>Fills the list of words with their translations.</summary>
        /// <param name="words">Words without translation.</param>
        /// <param name="userId">External user's identifier.</param>
        /// <returns>List of original words with translations from owner's vocabulary.</returns>
        public async Task<List<VocabWord>> FillVocabByWords(string[] words, int userId)
        {
            var vocabWords = new List<VocabWord>();
            var owner = _accountService.GetByExternalId(userId);

            vocabWords.AddRange(await _watchWordUnitOfWork.LearnWordsRepository.GetAllAsync(l => l.Owner.Id == owner.Id && words.Contains(l.Word)));
            vocabWords.AddRange(await _watchWordUnitOfWork.KnownWordsRepository.GetAllAsync(k => k.Owner.Id == owner.Id && words.Contains(k.Word)));

            foreach (var word in words.Where(word => vocabWords.All(v => v.Word != word)))
            {
                vocabWords.Add(new VocabWord { Word = word, Translation = "" });
            }

            return vocabWords;
        }
    }
}