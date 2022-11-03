using Bicode.Models;

namespace Bicode.Middleware;

public class MiddlewarePageNoFound
{
    private readonly RequestDelegate _next;
    public MiddlewarePageNoFound(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);
        if (context.Response.ContentType is null)
        {
            await context.Response.WriteAsJsonAsync(new ResponsePersonaDto
            {
                Message = "Pagina no encontrada",
                State = false
            });
        }
    }
}
