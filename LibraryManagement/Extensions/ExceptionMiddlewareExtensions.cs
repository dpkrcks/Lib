using Contracts;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;

namespace LibraryManagement.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger) {

            app.UseExceptionHandler(appError => {
                appError.Run(async context => { 
                   context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                  var contextFeature =  context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null) {
                        logger.logError($"Time:{DateTime.Now} , something went wrong , {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails { 
                            StatusCode = context.Response.StatusCode,
                            Message = "Something went wrong"
                        }.ToString());
                    }
                });
            });
        }
    }
}
