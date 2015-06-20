using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WatchWord.Web.UI.Models.Materials
{
    /// <summary>
    /// The add view model.
    /// </summary>
    public class AddMaterialViewModel
    {
        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        [Required(ErrorMessage = "Please select the file!")]
        public HttpPostedFileBase File { get; set; }
    }
}