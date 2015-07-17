using System.ComponentModel.DataAnnotations;
using System.Web;
using WatchWord.Domain.Entity;

namespace WatchWord.Web.UI.Models.Materials
{
    /// <summary>The add view model.</summary>
    public class AddMaterialViewModel
    {
        /// <summary>Gets or sets the file.</summary>
        [Required(ErrorMessage = "Please select the file!")]
        public HttpPostedFileBase File { get; set; }

        /// <summary>Gets or sets the image file.</summary>
        public HttpPostedFileBase Image { get; set; }

        /// <summary>Gets or sets material type.</summary>
        public MaterialType Type { get; set; }

        /// <summary>Gets or sets the name of the material.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets description of the material.</summary>
        public string Description { get; set; }

        //TODO: add attributes
    }
}