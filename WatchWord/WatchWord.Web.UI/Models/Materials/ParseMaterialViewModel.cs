using System.ComponentModel.DataAnnotations;
using System.Web;
using WatchWord.Domain.Entity;
using WatchWord.Web.UI.Infrastructure.ValidationAttributes;

namespace WatchWord.Web.UI.Models.Materials
{
    /// <summary>The ViewModel for the parse material view./></summary>
    public class ParseMaterialViewModel
    {
        /// <summary>Gets or sets the file.</summary>
        [MaxFileSize(1024 * 1024 * 4, ErrorMessage = "{0} file is too big.")]
        [Display(Name = "Subtitles")]
        [HttpPostedFileExtensions(Extensions = ".txt, .srt, .ass")]
        [Required]
        public HttpPostedFileBase File { get; set; }

        /// <summary>Gets or sets the number of the season.</summary>
        [Display(Name = "Season number")]
        [NotNullOrZeroIf("Type", "Series", ErrorMessage = "{0} is required.")]
        public int? SeasonNumber { get; set; }

        /// <summary>Gets or sets the episode number.</summary>
        [Display(Name = "Episode number")]
        [NotNullOrZeroIf("Type", "Series", ErrorMessage = "{0} is required.")]
        public int? EpisodeNumber { get; set; }

        /// <summary>Gets or sets the image file.</summary>
        [MaxFileSize(1024 * 1024 * 4, ErrorMessage = "{0} file is too big.")]
        [Display(Name = "Image")]
        [HttpPostedFileExtensions(Extensions = ".jpg, .bmp")]
        public HttpPostedFileBase Image { get; set; }

        /// <summary>Gets or sets the type of material.</summary>
        public MaterialType Type { get; set; }

        /// <summary>Gets or sets the name of the material.</summary>
        [Required]
        public string Name { get; set; }

        /// <summary>Gets or sets description of the material.</summary>
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}