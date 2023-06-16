using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UniversidadAPI.Helpers
{
    public sealed class HttpApiHelper : Controller
    {
        public async Task<ActionResult> SendAsync(HttpMethod mth, string urlBase, string endpoint, object content)
        {
            dynamic result;
            using (var client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }))
            {

                var msg = new HttpRequestMessage
                {
                    RequestUri = new Uri(urlBase + endpoint),
                    Method = mth
                };
                if (content != null)
                {
                    msg.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                }
                var req = await client.SendAsync(msg);


                if (!req.IsSuccessStatusCode)
                {
                    return StatusCode((int)req.StatusCode, req.ReasonPhrase);
                }
                var response = await req.Content.ReadFromJsonAsync<object>();
                if (response == null)
                {
                    return BadRequest("Empty or invalid response.");
                }
                result = response;
            }
            return Ok(result);
        }

        public static T Post<T>(string requestUri, object pars, string key)
        {
            T results;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Bearer",
                        key
                    );
                    var histResult = client.PostAsJsonAsync(requestUri, pars);
                    histResult.Wait();
                    var response = histResult.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    results = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString.Result);
                    return results;
                }
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        //Obtener token de otra API
        public static string GetAuth(string url, object pars)
        {
            using (var client = new HttpClient())
            {
                var task = client.PostAsJsonAsync(url, pars);
                task.Wait();
                var response = task.Result;
                var result = response.Content.ReadAsStringAsync();
                result.Wait();
                return result.Result;
            }
        }

        //Petición común a API
        public static T Get<T>(string url)
        {
            T model = default(T);
            using (var client = new HttpClient())
            {
                var task = client
                    .GetAsync(url)
                    .ContinueWith(
                        (taskwithresponse) =>
                        {
                            var response = taskwithresponse.Result;
                            var jsonString = response.Content.ReadAsStringAsync();
                            jsonString.Wait();
                            model = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(
                                jsonString.Result
                            );
                        }
                    );
                task.Wait();
            }
            return model;
        }

        //Petición con Auth a API
        public static T GetWAuth<T>(string url, string token)
        {
            T model = default(T);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    token
                );
                var task = client
                    .GetAsync(url)
                    .ContinueWith(
                        (taskwithresponse) =>
                        {
                            var response = taskwithresponse.Result;
                            var jsonString = response.Content.ReadAsStringAsync();
                            jsonString.Wait();
                            model = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(
                                jsonString.Result
                            );
                        }
                    );
                task.Wait();
            }
            return model;
        }
    }
}
