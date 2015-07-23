using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using WatchWord.Domain.Entity;
using WatchWord.Web.UI.Infrastructure.ValidationAttributes;

namespace WatchWord.Web.UI.Models.Materials
{
    /// <summary>The ViewModel for the parsematerial view./></summary>
    public class ParseMaterialViewModel
    {
        [HttpPostedFileRequired(ErrorMessage = "{0} is required")]
        [MaxFileSize(1024 * 1024 * 4, ErrorMessage = "{0} file is too big.")]
        [Display(Name = "Subtitles")]
        [HttpPostedFileExtensions(Extensions = ".txt, .srt, .ass")]
        /// <summary>Gets or sets the file.</summary>
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Season number")]
        [NotNullOrZeroIf("Type", "Series", ErrorMessage = "{0} is required.")]
        /// <summary>Gets or sets the number of the season.</summary>
        public int? SeasonNumber { get; set; }

        [Display(Name = "Episode number")]
        [NotNullOrZeroIf("Type", "Series", ErrorMessage = "{0} is required.")]
        /// <summary>Gets or sets the episode number.</summary>
        public int? EpisodeNumber { get; set; }

        [MaxFileSize(1024 * 1024 * 4, ErrorMessage = "{0} file is too big.")]
        [Display(Name = "Display image")]
        [HttpPostedFileExtensions(Extensions = ".jpg, .png, .bmp")]
        /// <summary>Gets or sets the image file.</summary>
        public HttpPostedFileBase Image { get; set; }

        /// <summary>Gets or sets the type of material.</summary>
        public MaterialType Type { get; set; }

        [Required]
        /// <summary>Gets or sets the name of the material.</summary>
        public string Name { get; set; }

        [Display(Name = "Description")]
        /// <summary>Gets or sets description of the material.</summary>
        public string Description { get; set; }
    }
}