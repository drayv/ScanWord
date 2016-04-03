using System.Web.Http;

namespace WatchWord.Web.UI.Controllers.WebApi
{
    public class ValuesController : ApiController
    {
        // GET api/Values/5
        [Authorize]
        public string Get(int id)
        {
            return "Value " + id;
        }
    }
}