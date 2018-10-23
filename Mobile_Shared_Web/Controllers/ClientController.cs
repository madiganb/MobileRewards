using Mobile_Shared_Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Mobile_Shared_Web.Controllers
{
    public class ClientController : BaseController
    {
        // GET: Client
        public ActionResult Index()
        {
            var clientResponse = GetRequest<List<ClientViewModel>>("clients/v1");

            if (clientResponse.Status == HttpStatusCode.NotFound)
            {
                return View(new List<ClientViewModel>());
            }

            return ProcessResults(clientResponse);
        }

        public ActionResult Details(string id)
        {
            var clientResponse = GetRequest<ClientViewModel>(string.Format("clients/v1/details/{0}", id));

            if (clientResponse.Status == HttpStatusCode.NotFound)
            {
                return View(new ClientViewModel { ClientName = "No Client Found" });
            }

            return ProcessResults(clientResponse);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ClientViewModel model)
        {
            model = model ?? new ClientViewModel();
            var validation = model.ValidateViewModel();

            if (!validation.IsValid)
            {
                model.ErrorMessage = validation.ValidationMessage;
                return View(model);
            }

            var result = PostRequest<ClientViewModel>("clients/v1", null, JsonConvert.SerializeObject(model));

            if (result.Status != HttpStatusCode.OK)
            {
                model.ErrorMessage = "Failed to create the client account";
                return View(model);
            }

            return RedirectToAction("Index", "Client");
        }

        [HttpPost]
        public ActionResult Details(ClientViewModel model)
        {
            model = model ?? new ClientViewModel();
            var validation = model.ValidateViewModel();

            if (!validation.IsValid)
            {
                model.ErrorMessage = validation.ValidationMessage;
                return View(model);
            }

            var result = PutRequest<ClientViewModel>("clients/v1", null, JsonConvert.SerializeObject(model));

            if (result.Status != HttpStatusCode.OK)
            {
                model.ErrorMessage = "Failed to update the client account";
                return View(model);
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            var result = DeleteRequest(string.Format("clients/v1/{0}", id), null);

            return RedirectToAction("Index", "Client");
        }
    }
}