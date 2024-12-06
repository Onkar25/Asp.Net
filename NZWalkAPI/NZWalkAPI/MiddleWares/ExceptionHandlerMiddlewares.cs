using System;
using System.Net;

namespace NZWalkAPI.MiddleWares
{
	public class ExceptionHandlerMiddlewares
	{
        private readonly ILogger<ExceptionHandlerMiddlewares> logger;
        private readonly RequestDelegate request;

        public ExceptionHandlerMiddlewares(ILogger<ExceptionHandlerMiddlewares> logger , RequestDelegate request)
		{
            this.logger = logger;
            this.request = request;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await request(httpContext);
            }
            catch (Exception ex)
            {
                var errorID = Guid.NewGuid();

                logger.LogError(ex, $"{errorID} : {ex.Message}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new {
                    Id = errorID,
                    ErrorMessage = "Something went wrong! We are looking into resolving this."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
	}
}

