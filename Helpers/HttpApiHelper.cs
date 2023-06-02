using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public static T Get<T>(string url)
        {
            T model = default(T);
            using (var client = new HttpClient())
            {
                var task = client.GetAsync(url).ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString.Result);
                    var a = model;
                });
                task.Wait();
            }
            return model;
        }
    }
}