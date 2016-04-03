using System.Collections.Generic;
using System.Web.Mvc;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.Entity;

namespace WatchWord.Web.UI.Controllers.Mvc
{
    /// <summary>Settings controller.</summary>
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;

        /// <summary>Initializes a new instance of the <see cref="VocabularyController"/> class.</summary>
        /// <param name="settingsService">Settings service.</param>
        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        /// <summary>Settings index page.</summary>
        /// <returns>Settings page.</returns>
        [Authorize]
        public ActionResult Index()
        {
            //TODO: viewModel
            var unfilledAdminSettings = _settingsService.GetUnfilledSiteSettings();
            return View(unfilledAdminSettings);
        }

        /// <summary>Saves settings to storage.</summary>
        /// <param name="model">Settings model.</param>
        /// <returns>Settings page redirect.</returns>
        [HttpPost]
        [Authorize]
        public ActionResult SaveSettings(List<Setting> model)
        {
            //TODO: work with all types of settings (not only admin)
            _settingsService.InsertSiteSettings(model);
            return RedirectToAction("Index");
        }
    }
}