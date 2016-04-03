using System.Web.Http;

namespace WatchWord.Web.UI.Controllers.WebApi
{
    public class TestsController : ApiController
    {
        // GET api/Tests/5
        public string Get(int id)
        {
            return "Test " + id;
        }
    }
}