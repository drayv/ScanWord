using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WatchWord.Application.EntityServices.Abstract;

namespace WatchWord.Web.UI.Controllers.WebApi
{
    public class MaterialImageController : ApiController
    {
        private readonly IMaterialsService _materialService;
        private TimeSpan _age;

        /// <summary>Initializes a new instance of the <see cref="MaterialImageController"/> class.</summary>
        /// <param name="materialService">Material service.</param>
        public MaterialImageController(IMaterialsService materialService)
        {
            _materialService = materialService;
            _age = new TimeSpan(120, 0, 0);
        }

        public HttpResponseMessage Get(int id)
        {
            // Check if exist 
            var materialImage = _materialService.GetMaterial(id);
            if (materialImage == null || materialImage.Image == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            // Check if correct mime type 
            MediaTypeHeaderValue mimeTypeHeader;
            try
            {
                mimeTypeHeader = new MediaTypeHeaderValue(materialImage.MimeType);
            }
            catch (System.FormatException)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            // Create response 
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(new MemoryStream(materialImage.Image))
            };

            result.Content.Headers.ContentType = mimeTypeHeader;
            result.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = _age,
                Public = false,
                NoCache = false,
                Private = true,
            };
            result.Content.Headers.Expires = DateTime.UtcNow.Add(_age);

            return result;
        }
    }
}