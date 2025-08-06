using Faqidy.APIs.Errors;
using Faqidy.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Faqidy.APIs.Middlewares
{
    public class ExeptionHandlerMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExeptionHandlerMiddleware(RequestDelegate next, ILogger<ExeptionHandlerMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // logic 
                await _next(context);
                // logic
                #region Handle not found edpoint 
                //// handle not found endpoint 
                //if(context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                //{
                //    context.Response.ContentType = "application/json";
                //    var response = new ApiResponse((int)HttpStatusCode.NotFound, $"The Requested endpoint: {context.Request.Path} is not found.");
                //    await context.Response.WriteAsync(response.ToString());
                //} 
                #endregion
            }
            catch(NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse((int)HttpStatusCode.NotFound , ex.Message);
                await context.Response.WriteAsync(response.ToString());

            }
            catch(BadRequestException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse((int) HttpStatusCode.BadRequest , ex.Message);
                await context.Response.WriteAsync(response.ToString());
            }
            catch (Exception ex)
            {
                #region Loggign TODO
                // log exeption in env mode 
                if (_environment.IsDevelopment())
                {
                    _logger.LogError(ex, ex.Message, ex.StackTrace);
                }
                else
                {
                    // log in external file or database [in production]
                }
                #endregion

                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _environment.IsDevelopment() ? new ExeptionHandlerErrorResponse((int)HttpStatusCode.InternalServerError
                                , ex.Message, ex.StackTrace) : new ExeptionHandlerErrorResponse((int) HttpStatusCode.InternalServerError,
                                ex.Message);

                await context.Response.WriteAsync(response.ToString());
            }

            
        }
    }

    public static class ExeptionHandlerForMIddlewareExetentionMethod
    {
        public static IApplicationBuilder UseExeptionHandlerMiddleware(this  IApplicationBuilder app)
        {
            app.UseMiddleware<ExeptionHandlerMiddleware>();

            return app;
        }
    }
}
