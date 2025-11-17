using eCommerceApp.Application.Services.Interfaces.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
namespace eCommerceApp.Infrastructure.Middelwares
{ 
  public class ExceptionHandlingMiddleware(RequestDelegate _next)
  { 
      public async Task InvokeAsync(HttpContext context)
      {
          try {

            await _next(context);

          } catch (DbUpdateException ex)  { 

                var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();
               context.Response.ContentType = "application/json";

                if (ex.InnerException is SqlException innerException)
                { 
                    logger.LogError(innerException, "Sql Exception");

                    switch (innerException.Number) 
                    { 
                        case 2627: //Unique CVonstraint Violation"
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsync("Unique CVonstraint Violation");
                            break; 
                        

                        case 515: //Cannot insert null
                            context.Response.StatusCode = StatusCodes.Status409Conflict; 
                            await context.Response.WriteAsync("Cannot insert null");
                            break; 


                        case 547: //Foreign key constraint violation
                              context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsync("Foreign key constraint violation");
                            break;
                        
                        
                        default : context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsync("An error occured while processing your request");
                            break; 
                    } 
                
                } else 
                {
                             logger.LogError(ex, "Related EfCore Exception");
                             context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                             await context.Response.WriteAsync("An error occured while saving entity changes");
                }
          } catch (Exception ex) 
          { 
                var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();
                logger.LogError(ex, "Unknown Exception");
                context.Response.ContentType = "application/json"; 
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An error occured "+ex.Message);
            
          } 
        } 
    }
}