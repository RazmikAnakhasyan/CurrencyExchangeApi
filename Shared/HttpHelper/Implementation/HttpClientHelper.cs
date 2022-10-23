using System.Formats.Asn1;
using System.Text.Json;
using System.Web;

namespace Shared.HttpHelper.Implementation
{
    public static class HttpClientHelper<T, TRes> where T : class where TRes : class
    {
        public static async Task<TRes> GetRequestAsync(string baseUrl, string actionLink, IDictionary<string, string> headers)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            foreach (var header in headers)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            var request = httpClient.GetAsync(actionLink, CancellationToken.None);
            var content = request.Result.Content;
            var json = await content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TRes>(json);
        }

        public static async Task<TRes> GetRequestAsync(string baseUrl, string actionLink,T requestModel, IDictionary<string, string> headers)
        {
            var httpClient = new HttpClient();
            var a = requestModel.ToQueryString();
            httpClient.BaseAddress = new Uri($"{baseUrl}{actionLink}?{requestModel.ToQueryString()}");  
            foreach (var header in headers)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            var request = httpClient.GetAsync("");
            var content = request.Result.Content;
            var json = await content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TRes>(json);
        }

        #region Private Methods
       
        #endregion
    }


}
