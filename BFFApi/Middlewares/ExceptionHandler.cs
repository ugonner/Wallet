namespace BFFApi.Middlewares;
using Contracts.ServiceContracts;
using Shared;

using Microsoft.AspNetCore.Diagnostics;
using System.Net;

public static class ConfigureExceptiionHandlerExtension
{
    public static void ConfigureExceptiionHandler(this WebApplication app, ILoggerManager loggerManager)
    {
        app.UseExceptionHandler(errApp => 
        {
            GenericResult<string> response = new GenericResult<string>();
            
                errApp.Run(async (HttpContext context) => 
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>();
                    if(exception != null)
                    {
                        loggerManager.LogError($"SOMETHING WENT WRONG: {exception.Error}");

                    context.Response.StatusCode = 500;
                     response.Errored("something went wrong, try later", 500);
                     context.Response.WriteAsync(response.ToString());
                    }
                });
            
        });
    }
}