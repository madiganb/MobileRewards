using System.Net;
using System.Net.Http;

namespace Mobile_Shared_Web.Models
{
    public class ServiceResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public T Content { get; set; }

        public ServiceResponse(HttpResponseMessage response)
        {
            Status = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                Content = response.Content.ReadAsAsync<T>().Result;
            }
            else
            {
                Content = default(T);
            }
        }
    }
}