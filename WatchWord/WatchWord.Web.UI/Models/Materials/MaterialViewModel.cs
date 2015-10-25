using System;
using System.Linq;
using WatchWord.Domain.Entity;

namespace WatchWord.Web.UI.Models.Materials
{
    public class MaterialViewModel
    {
        public MaterialViewModel(Material material, int width, int height)
        {
            if (material == null)
            {
                throw new ArgumentNullException("material");
            }

            InitializeFields(material, width, height);
        }

        public string Image { get; set; }

        public string ImageWidth { get; set; }

        public string ImageHeight { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string[] Words { get; set; }

        private void InitializeFields(Material material, int width, int height)
        {
            Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(material.Image));
            Name = material.Name;
            Description = material.Description ?? string.Empty;
            ImageWidth = width.ToString();
            ImageHeight = height.ToString();
            Words = material.File.Words == null ? new string[0] : material.File.Words.Select(n => n.TheWord).ToArray();
        }
    }
}