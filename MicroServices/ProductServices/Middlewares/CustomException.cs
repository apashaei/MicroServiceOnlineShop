using App.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ProductServices.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomException
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<CustomException> _logger;
        private readonly IMetrics metrics;

        public CustomException(RequestDelegate next, ILogger<CustomException> logger, IMetrics metrics)
        {
            _next = next;
            _logger = logger;
            this.metrics = metrics; 
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                metrics.Measure.Counter.Increment(new App.Metrics.Counter.CounterOptions
                {
                    Name = "Errors"
                });
                _logger.LogError(ex.Message);
                await Task.FromResult(ex.Message.ToString());
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomExceptionExtensions
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomException>();
        }
    }
}
