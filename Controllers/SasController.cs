using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.ServiceBus;

namespace event_hubs_sas_generator.Controllers
{
    public class SasController : Controller
    {
        // GET: Sas
        public ActionResult Index()
        {
            return View();
        }

        // POST: Sas/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var sasNamespace = collection["Namespace"];
            var path = collection["Path"];
            var policy = collection["Policy"];
            var key = collection["Key"];
            var expiration = collection["Expiration"];

            var parsedExpiration = DateTime.Parse(expiration);
            var ttl = parsedExpiration.Subtract(DateTime.UtcNow);

            var serviceUri = ServiceBusEnvironment.CreateServiceUri("https", sasNamespace, path).ToString().Trim('/');
            var sasToken = SharedAccessSignatureTokenProvider.GetSharedAccessSignature(policy, key, serviceUri, ttl);
            
            var encodedToken = HttpServerUtility.UrlTokenEncode(Encoding.ASCII.GetBytes(sasToken));

            return RedirectToAction("DisplaySas", new { id = encodedToken });
        }

        public ActionResult DisplaySas(string id)
        {
            var decodedToken = Encoding.ASCII.GetString(HttpServerUtility.UrlTokenDecode(id));

            ViewData["sas"] = decodedToken;
            return View();
        }

    }
}
