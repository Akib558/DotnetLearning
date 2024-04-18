using MiddlwareLearning.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<MyMiddleware>();


var app = builder.Build();

// app.MapGet("/", () => "Hello World!");


// app.UseMiddleware<MyMiddleware>();

app.MyMiddleware();
// app.MyMiddleware2();


// app.MyMiddle



app.UseWhen(
    context => context.Request.Query.ContainsKey("IsAuthorized") && context.Request.Query["IsAuthorized"] == "true",
    // app => app.Use(
    //     async (context, next) =>
    //     {
    //         await context.Response.WriteAsync("Hello, this response is directly from the Run method of the app object.");
    //         await next();
    //     }
    // )
    app => app.MyMiddleware2()
);



app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello, this response is directly from the Run method of the app object.");
    await next(context);
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello, this response is directly from the Run method of the app object, second response");
});

app.Run();