using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Net;
using System.Net.Http.Headers;

namespace APiGatWayWebApi.Models.Aggregators
{
    public class ProductBasketAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var response1 = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var response2 = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var combinedResponse = $"{{ \"service1\": {response1}, \"service2\": {response2} }}";


            var stringContent = new StringContent(combinedResponse)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<Header>(), "OK");

        }
    }
}
