using System;
using System.Web.Mvc;

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
            var lifespan = parsedExpiration.Subtract(DateTime.UtcNow);

            //var serviceUri = ServiceBusEnvironment.CreateServiceUri("https", sasNamespace, path).ToString().Trim('/');
            //var sasToken = SharedAccessSignatureTokenProvider.GetSharedAccessSignature(policy, key, serviceUri, lifespan);
            var sasToken = "abc123";

            return RedirectToAction("DisplaySas", new {id = sasToken});
        }

        public ActionResult DisplaySas(string id)
        {
            ViewData["sas"] = id;
            return View();
        }

    }
}
