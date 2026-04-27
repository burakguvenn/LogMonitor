using LogMonitor.DataAccess.Concrete.Context;
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
        if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("API Key was not provided.");
            return;
        } 

        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ApiKey == extractedApiKey.ToString() && u.IsActive);

        if (user == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized user.");
            return;
        }

        var clientIp = context.Connection.RemoteIpAddress?.ToString();
        
        if (string.IsNullOrEmpty(clientIp))
        {
            context.Response.StatusCode = 403; // Forbidden
            await context.Response.WriteAsync("Could not determine client IP address");
            return;
        }

        if (string.IsNullOrEmpty(user.AllowedIPs))
        {
            user.AllowedIPs = clientIp;
            await dbContext.SaveChangesAsync();
        }
        else
        {
            if (user.AllowedIPs != clientIp)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden: API Key is restricted to a different IP address.");
                return;
            }
        }

        context.Items["UserId"] = user.Id;

        await _next(context);
    }
}