using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Monitoring.ExternalAdapters.Exceptions;
using Monitoring.Repository.Exceptions;
using MonitoringApi.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace MonitoringApi.Filters
{
    /// <summary>
    /// Classe de gerenciamento centralizado de exceções
    /// </summary>
    public class ExceptionHandling : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            ErrorModel model;
            HttpResponse response = context.HttpContext.Response;
            response.ContentType = "application/json";

            if (exception is CustomValidationException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                model = new ErrorModel(HttpStatusCode.BadRequest, (exception as CustomValidationException).Messages);
            }
            else
            if (exception is AggregateException && exception.InnerException is ExternalApiException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                model = new ErrorModel(HttpStatusCode.BadRequest, (exception.InnerException as ExternalApiException).Message);
            }
            else
            if (exception is NotImplementedException)
            {
                response.StatusCode = (int)HttpStatusCode.NotImplemented;
                model = new ErrorModel(HttpStatusCode.NotImplemented, "Funcionalidade não implementada");
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                model = new ErrorModel(HttpStatusCode.InternalServerError, "Erro interno");
            }

            context.Result = new JsonResult(model);
        }
    }
}
