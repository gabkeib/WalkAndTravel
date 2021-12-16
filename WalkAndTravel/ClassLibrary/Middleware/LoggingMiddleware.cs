using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var controllerActionDescriptor =
                context
                .GetEndpoint()
                .Metadata
                .GetMetadata<ControllerActionDescriptor>();

            var controllerName = controllerActionDescriptor.ControllerName;
            var actionName = controllerActionDescriptor.ActionName;

            await _next(context);

            _logger.Information($"Action {actionName} invoked in controller {controllerName}");
        }
    }
}
