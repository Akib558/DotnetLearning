using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlwareLearning.CustomMiddleware
{
    public class MyMiddleware2
    {
        private readonly RequestDelegate _next;

        public MyMiddleware2(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Your middleware logic goes here
            await context.Response.WriteAsync("Hello, this response is directly from myMiddlware2 the Run method of the app object.");

            await _next(context); // Call the next middleware in the pipeline
        }
    }

    public static class MyMiddleware2Extensions
    {
        public static IApplicationBuilder MyMiddleware2(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware2>();
        }
    }
}