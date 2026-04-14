using LogMonitor.API.Data;
using Microsoft.EntityFrameworkCore;

namespace LogMonitor.API.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string APIKEYNAME = "x-api-key";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        // Request header'ında apikey var mı kontrolü
        if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("API Key was not provided.");
            return;
        } 

        // Alınan key'i db'de bulunuyor mu
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ApiKey == extractedApiKey.ToString() && u.IsActive);

        if (user == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized user.");
            return;
        }

        context.Items["UserId"] = user.Id;

        await _next(context);
    }
}