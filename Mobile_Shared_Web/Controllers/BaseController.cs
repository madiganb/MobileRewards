using Mobile_Shared_Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace Mobile_Shared_Web.Controllers
{
    public class BaseController : Controller
    {
        public virtual ActionResult ProcessResults<T>(ServiceResponse<T> response)
        {
            switch (response.Status)
            {
                case System.Net.HttpStatusCode.OK:
                    return View(response.Content);

                case System.Net.HttpStatusCode.NotFound:
                    return View(default(T));

                case System.Net.HttpStatusCode.InternalServerError:
                    return Redirect("~/Error");

                default:
                    return View(default(T));
            }
        }

        public ServiceResponse<T> GetRequest<T>(string url, Dictionary<string, string> headers = null)
        {
            url = string.Format("{0}{1}", Settings.BaseUrl, url);

            using (var client = new HttpClient())
            {                
                if (headers != null)
                {
                    foreach(var kvp in headers)
                    {
                        client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                    }
                }                

                var response = client.GetAsync(url).Result;

                return new ServiceResponse<T>(response);
            }
        }

        public ServiceResponse<T> PostRequest<T>(string url, Dictionary<string, string> headers, string body)
        {
            url = string.Format("{0}{1}", Settings.BaseUrl, url);

            using (var client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var kvp in headers)
                    {
                        client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                    }
                }

                var response = client.PostAsJsonAsync(url, body).Result;

                return new ServiceResponse<T>(response);
            }
        }

        public ServiceResponse<T> PutRequest<T>(string url, Dictionary<string, string> headers, string body)
        {
            url = string.Format("{0}{1}", Settings.BaseUrl, url);

            using (var client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var kvp in headers)
                    {
                        client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                    }
                }

                var response = client.PutAsJsonAsync(url, body).Result;

                return new ServiceResponse<T>(response);
            }
        }

        public bool DeleteRequest(string url, Dictionary<string, string> headers)
        {
            url = string.Format("{0}{1}", Settings.BaseUrl, url);

            using (var client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var kvp in headers)
                    {
                        client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                    }
                }

                var response = client.DeleteAsync(url).Result;

                return response.IsSuccessStatusCode;
            }
        }
    }
}