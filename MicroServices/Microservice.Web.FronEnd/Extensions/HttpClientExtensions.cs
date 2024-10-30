using System.Text.Json;

namespace Microservice.Web.FronEnd.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong when calling api, {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync();

            var jsonData = JsonSerializer.Deserialize<T>(dataAsString)!;
            return jsonData;
        }
    }
}
