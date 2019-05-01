using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using StarWarsSampleApp.Application.Exceptions;

namespace StarWarsSampleApp.Application.Infrastructure
{
    public class ExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode = (context.Exception is WebException exception &&
                                         ((HttpWebResponse)exception.Response) != null) ?
                ((HttpWebResponse)exception.Response).StatusCode
                : GetErrorCode(context.Exception.GetType());
            string errorMessage = context.Exception.Message;
            string stackTrace = context.Exception.StackTrace; // for logging

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(
                new
                {
                    message = errorMessage,
                    errorCode = statusCode
                });

            //TODO Logging

            response.ContentLength = result.Length;
            response.WriteAsync(result);
        }

        private HttpStatusCode GetErrorCode(Type exceptionType)
        {
            if (exceptionType == typeof(NotFoundException))
            {
                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
