using System;
using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Web.UI.Models.Materials
{
    public class MaterialViewModel
    {
        public string Image { get; set; }

        public string ImageWidth { get; set; }

        public string ImageHeight { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<VocabWord> Words { get; set; }

        public int Counter;

        public MaterialViewModel(Material material, List<VocabWord> vocabWords, int width, int height)
        {
            if (material == null)
            {
                throw new ArgumentNullException("material");
            }

            InitializeFields(material, vocabWords, width, height);
        }

        private void InitializeFields(Material material, List<VocabWord> vocabWords, int width, int height)
        {
            Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(material.Image));
            Name = material.Name;
            Description = material.Description ?? string.Empty;
            ImageWidth = width.ToString();
            ImageHeight = height.ToString();
            Words = vocabWords;
            Counter = 1;
        }
    }
}