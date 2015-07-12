using System.Collections.Generic;
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
        /// Initializes a new instance of the <see cref="AddMaterialViewModel"/> class.
        /// </summary>
        public AddMaterialViewModel()
        {
            this.Words = new HashSet<string>();
        }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        [Required(ErrorMessage = "Please select the file!")]
        public HttpPostedFileBase File { get; set; }

        /// <summary>
        /// Gets or sets the list of words.
        /// </summary>
        public IEnumerable<string> Words { get; set; }
    }
}