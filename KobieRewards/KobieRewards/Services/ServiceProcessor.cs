using System.Collections.Generic;
using System.Net.Http;

namespace KobieRewards.Services
{
    public class ServiceProcessor
    {
        public ServiceResponse<T> GetRequest<T>(string url, Dictionary<string, string> headers = null)
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

                var response = client.PostAsync(url, new StringContent(body)).Result;

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

                var response = client.PutAsync(url, new StringContent(body)).Result;

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
