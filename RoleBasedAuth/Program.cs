using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

Dictionary<String, List<String>> gamesMap = new(){
    {"Action", new List<String>{"Call of Duty", "Battlefield", "Halo"}},
    {"RPG", new List<String>{"The Witcher", "Final Fantasy", "Elder Scrolls"}},
    {"Sports", new List<String>{"FIFA", "NBA 2K", "Madden"}}
};


Dictionary<String, List<String>> subscription = new(){
    {"silver", new List<String>{"Action"}},
    {"gold", new List<String>{"Action", "RPG"}},
    {"platinum", new List<String>{"Action", "RPG", "Sports"}}
};

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        // options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(
    x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = "https://localhost:5001",
            ValidAudience = "https://localhost:5001",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
        };
    }
);
builder.Services.AddAuthorization();


var app = builder.Build();




app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/playergames", () => gamesMap)
    .RequireAuthorization(policy =>
    {
        policy.RequireRole("admin");
    });

app.MapGet("/mygames", (ClaimsPrincipal user) =>
{
    var hasClaim = user.HasClaim(c => c.Type == "subscription");

    if (hasClaim)
    {
        var subscription = user.FindFirstValue("subscription");
        return Results.Ok(subscription);
    }
    ArgumentNullException.ThrowIfNull(user.Identity?.Name);
    var userName = user.Identity.Name;
    if (gamesMap.ContainsKey(userName) == false)
    {
        return Results.Empty;
    }
    return Results.Ok(gamesMap[userName]);
}).
RequireAuthorization(policy =>
{
    policy.RequireRole("player");
});

app.Run();
