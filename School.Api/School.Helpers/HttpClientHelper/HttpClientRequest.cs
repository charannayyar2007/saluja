using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace School.Helpers.HttpClientHelper
{

    public  class HttpClientRequest<TSource>
    {
        public static HttpClient client = new HttpClient();

       public HttpClientRequest()
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["apiUrl"]);
             client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

       public static async Task<HttpResponseMessage> CreateProductAsync(TSource product,string url)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(url, product);  // Blocking call! Program will wait here until a response is received or a timeout occurs.
         
            //response.EnsureSuccessStatusCode();
            //if (response.IsSuccessStatusCode)
            //{
            //    // Parse the response body.
            //    return response.Content.ReadAsAsync<TResult>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll

            //}
            // return URI of the created resource.
            return response;
        }

        public static async Task<HttpResponseMessage> GetProductAsync(string path)
        {
           // T product = null;
            HttpResponseMessage response = await client.GetAsync(path);
           
            return response;
        }

        public static async Task<HttpResponseMessage> UpdateProductAsync(TSource product,string url)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
               url, product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            return response;


        }

        public static async Task<HttpStatusCode> DeleteProductAsync(string id,string url)
        {
            HttpResponseMessage response = await client.DeleteAsync(
              url);
            return response.StatusCode;
        }

    }
}
