using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Abstract
{
    public interface IVocabularyService
    {
        Task<List<KnownWord>> GetKnownWords(int userId);

        Task<List<LearnWord>> GetLearnWords(int userId);

        int InsertKnownWord(string word, string translation, int userId);

        int InsertLearnWord(string word, string translation, int userId);
    }
}