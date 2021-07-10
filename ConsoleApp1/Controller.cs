using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Controller
    {
        public async Task<dynamic> Mountebank()
        {
            var appsettings = "http://localhost:2525/imposters/57298";
            var httpclient = new HttpClient();

            var result =
                await httpclient.GetAsync(appsettings);
            string responseBody = await result.Content.ReadAsStringAsync();
            var jobject = JsonConvert.DeserializeObject<JObject>(responseBody);
            Console.WriteLine(result);
            return result;
        }
        //public async Task<ApiResponse<T>> SendGetRequestAsync<T>(string uriPath, string queryString)
        //{
        //    return await SendRequestAsync<T>(HttpMethod.Get, "", uriPath, queryString);
        //}
    }
}
