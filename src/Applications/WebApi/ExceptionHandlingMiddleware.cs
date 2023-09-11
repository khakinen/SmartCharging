using SmartCharging.Domain.Contract.Exceptions;

namespace SmartCharging.Application.WebApi;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException ex)
        {
            await HandleException(context, StatusCodes.Status404NotFound, ex.Message);
        }

        catch (ValidationException ex)
        {
            await HandleException(context, StatusCodes.Status400BadRequest ,ex.Message);
        }
        catch (ConflictException ex)
        {
            await HandleException(context, StatusCodes.Status409Conflict, ex.Message);
        }
        catch (Exception ex)
        {
            await HandleException(context, StatusCodes.Status500InternalServerError, ex.Message);
            
            _logger.LogError(ex.Message);
        }
    }

    private async Task HandleException(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        
        await context.Response.WriteAsync(message);
    }
}