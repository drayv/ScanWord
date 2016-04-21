using System;
using System.Collections.Generic;
using WatchWord.Domain.Entity;

namespace WatchWord.Web.UI.Models.Materials
{
    public class MaterialViewModel
    {
        public string Image { get; set; }

        public string ImageSource { get; set; }

        public string ImageWidth { get; set; }

        public string ImageHeight { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<VocabWord> Words { get; set; }

        public MaterialViewModel(Material material, List<VocabWord> vocabWords, int imageWidth, int imageHeight, string imgSrc)
        {
            if (material == null)
            {
                throw new ArgumentNullException(nameof(material));
            }

            //TODO: fix this sh%^&
            ImageSource = imgSrc;
            Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(material.Image));

            Name = material.Name;
            Description = material.Description ?? string.Empty;
            ImageWidth = imageWidth.ToString();
            ImageHeight = imageHeight.ToString();
            Words = vocabWords;
        }
    }
}