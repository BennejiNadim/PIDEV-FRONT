using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PIDEV_FRONTEND
{
    public static class WebApiHelper
    {

        private static HttpClient getHttpClient()
        {
            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri("http://localhost:8081");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public static T Get<T>(string api, Action<HttpClient> beforeGet = null)
        {
            using (HttpClient httpClient = getHttpClient())
            {
                if (beforeGet != null)
                {
                    beforeGet(httpClient);
                }

                var response = httpClient.GetAsync(api).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
            }

            return default;
        }

        public static T Post<T>(string api, T payload)
        {
            using (HttpClient httpClient = getHttpClient())
            {

                var response = httpClient.PostAsJsonAsync(api, payload)
                    .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
            }

            return default;
        }

        public static T Put<T>(string api, T payload)
        {
            using (HttpClient httpClient = getHttpClient())
            {

                var response = httpClient.PutAsJsonAsync(api, payload)
                    .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
            }

            return default;
        }

        public static T Delete<T>(string api)
        {
            using (HttpClient httpClient = getHttpClient())
            {

                var response = httpClient.DeleteAsync(api)
                    .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
            }

            return default;
        }
    }
}