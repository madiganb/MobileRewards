using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace KobieRewards.Services
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
                var result = response.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrWhiteSpace(result))
                {
                    Content = default(T);
                }

                Content = JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                Content = default(T);
            }
        }
    }
}
