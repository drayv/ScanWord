using System;
using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Web.UI.Models.Materials
{
    public class DisplayAllViewModel
    {
        public int CurrentPageNumber { get; set; }

        public int TotalPagesCount { get; set; }

        /// <summary>The list of materials to view.</summary>
        public IEnumerable<Material> Materials { get; set; }

        /// <summary>MaterialId -> MaterialStatisticViewModel dictionary.</summary>
        public Dictionary<int, MaterialStatisticViewModel> MaterialsStatistic { get; set; }

        public DisplayAllViewModel(int pageSize, int pageNumber, int totalCount,
            IEnumerable<Material> materials, Dictionary<int, MaterialStatisticViewModel> statistic)
        {
            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException("pageSize", "Can't be lower then 0.");
            }

            if (materials == null)
            {
                throw new ArgumentNullException("materials");
            }

            InitializeFields(pageSize, pageNumber, totalCount, materials, statistic);
        }

        private void InitializeFields(int pageSize, int pageNumber, int totalCount,
            IEnumerable<Material> materials, Dictionary<int, MaterialStatisticViewModel> statistic)
        {
            CurrentPageNumber = pageNumber > 0 ? pageNumber : 1;
            TotalPagesCount = (int)Math.Ceiling((double)totalCount / pageSize);
            Materials = materials;
            MaterialsStatistic = statistic;
        }
    }

    public class MaterialStatisticViewModel
    {
        public int AllWordsCount { get; set; }
    }
}