using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Web.UI.Models.Vocabulary
{
    public class DisplayAllViewModel
    {
        public IEnumerable<KnownWord> KnownWords { get; set; }

        public IEnumerable<LearnWord> LearnWords { get; set; }

        public int Counter;

        public DisplayAllViewModel()
        {
            Counter = 1;
        }
    }
}