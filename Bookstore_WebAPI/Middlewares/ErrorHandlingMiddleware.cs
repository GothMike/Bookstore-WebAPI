using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Bookstore_WebAPI.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // логирование исключения
                _logger.LogError(ex, ex.Message);

                // отправка пользователю сообщения об ошибке
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails errorResponse = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Ошибка сервера",
                    Title = "Ошибка сервера",
                    Detail = "Произошла ошибка, попробуй повторить свой запрос позже",
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }
    }
}
