using OCASAPI.Application.Exceptions;
using OCASAPI.Application.Wrappers;
using System.Net;
using System.Text.Json;


namespace OCASAPI.WebAPI.Middleware
{
    /// <summary>
    /// Error Handling Middleware, to catch any errors or exceptions that occurred before
    /// or after the request was processed as part of the request middleware
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Error handling middleware to catch and return errors from request pipeline
        /// </summary>
        /// <param name="next">Instance of <see cref="Microsoft.AspNetCore.Http.RequestDelegate"/></param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke next request in the pipeline, if there was an error return an exception
        /// </summary>
        /// <param name="context"> Instance of <see cref="Microsoft.AspNetCore.Http.HttpContext"/></param>
        /// <returns>pass instance of <see cref="Microsoft.AspNetCore.Http.HttpContext"/> to next request delegate <see cref="Microsoft.AspNetCore.Http.RequestDelegate" /></returns>
        /// <exception cref="System.Exception">
        /// <pararef name="context"/> is null
        /// </exception>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>(error.Message) { Succeeded = false};
                
                switch (error)
                {
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ApiExceptions e:
                        //general exception
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ValidationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.OK;
                        responseModel.Errors = e.Errors;
                        break;
                    case Exception e:
                        //general exception
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Message = "There was an error processing the request";
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}