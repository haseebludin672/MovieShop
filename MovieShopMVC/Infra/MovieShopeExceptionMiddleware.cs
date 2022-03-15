using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Serilog.AspNetCore;
using Serilog;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Events;

namespace MovieShopMVC.Infra
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MovieShopeExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MovieShopeExceptionMiddleware> _logger;

        public MovieShopeExceptionMiddleware(RequestDelegate next, ILogger<MovieShopeExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            //logic 
            _logger.LogInformation("Inside exception Middleware");
            //if there is no exception then call next middleware
            //if there is exception then do the logging

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger?.LogError("Excetpin happend, handle here");
                // if there is Exception then do the logging

               await HandleExceptionLogic(httpContext, ex);
            }
        }
        
        private async Task HandleExceptionLogic(HttpContext httpContext,Exception exception )
        {
            _logger.LogError("Something went worng");
            //get the exception details
            var exceptionDetaisl = new
            {
                ExceptionMessage = exception.Message,
                ExceptionStackTrace = exception.StackTrace,
                ExceptionType = exception.GetType(),
                ExceptionDetails = exception.InnerException?.Message,
                ExceptionDateTime = DateTime.UtcNow,
                Path = httpContext.Request.Path,
                HttpMethod = httpContext.Request.Method,
                User = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null,
  
            };



            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
             .Enrich.FromLogContext()
             .WriteTo.Console()
             .CreateBootstrapLogger(); // <-- Change this line!


            //log the above object details to text or json file using serilog
            _logger.LogError(exceptionDetaisl.ExceptionMessage);
            httpContext.Response.Redirect("/home/error");
            await Task.CompletedTask;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MovieShopeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMovieShopeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopeExceptionMiddleware>();
        }
    }
}
