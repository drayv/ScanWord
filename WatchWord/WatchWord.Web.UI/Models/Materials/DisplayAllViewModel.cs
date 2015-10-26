using System;
using System.Collections.Generic;
using WatchWord.Domain.Entity;
using System.Linq;

namespace WatchWord.Web.UI.Models.Materials
{
    public class DisplayAllViewModel
    {

        public DisplayAllViewModel(int pageSize, int pageNumber, IEnumerable<Material> materials)
        {
            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException("pageSize", "Can't be lower then 0.");
            }

            if (materials == null)
            {
                throw new ArgumentNullException("materials");
            }

            InitializeFields(pageSize, pageNumber, materials);
        }

        private void InitializeFields(int pageSize, int pageNumber, IEnumerable<Material> materials)
        {
            CurrentPageNumber = pageNumber > 0 ? pageNumber : 1;
            TotalPagesCount = (int)Math.Ceiling((decimal)materials.Count() / pageSize);
            Materials = materials.Skip(pageSize * (CurrentPageNumber - 1)).Take(pageSize);
        }

        public int CurrentPageNumber { get; set; }

        public int TotalPagesCount { get; set; }

        public IEnumerable<Material> Materials { get; set; }
    }
}