namespace MiddlwareLearning.CustomMiddleware
{
    public class MyMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello, this response is directly from myMiddlware the Run method of the app object.");
            await next(context);
        }
    }
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder MyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}