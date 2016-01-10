using System.Collections.Generic;
using System.Web.Mvc;
using WatchWord.Application.EntityServices.Abstract;
using WatchWord.Domain.Entity;

namespace WatchWord.Web.UI.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;

        /// <summary>Initializes a new instance of the <see cref="VocabularyController"/> class.</summary>
        /// <param name="settingsService">Settings service.</param>
        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [Authorize]
        public ActionResult Index()
        {
            //TODO: viewModel
            var unfilledAdminSettings = _settingsService.GetUnfilledAdminSettings();
            return View(unfilledAdminSettings);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveSettings(List<Setting> model)
        {
            //TODO: work with all types of settings (not only admin)
            _settingsService.InsertAdminSettings(model);
            return RedirectToAction("Index");
        }
    }
}