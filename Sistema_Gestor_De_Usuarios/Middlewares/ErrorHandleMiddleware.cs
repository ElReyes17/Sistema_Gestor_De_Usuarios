using Sistema_Gestor_De_Usuarios.Core.Application.Common;
using System.Net;
using System.Text.Json;

namespace Sistema_Gestor_De_Usuarios.Middlewares
{
    public class ErrorHandlerMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new { mensaje = error.Message }; ;

                response.StatusCode = error switch
                {
                    ApiException e => e.ErrorCode switch
                    {
                        (int)HttpStatusCode.BadRequest => (int)HttpStatusCode.BadRequest,
                        (int)HttpStatusCode.InternalServerError => (int)HttpStatusCode.InternalServerError,
                        (int)HttpStatusCode.NotFound => (int)HttpStatusCode.NotFound,
                        (int)HttpStatusCode.NoContent => (int)HttpStatusCode.NoContent,
                        _ => (int)HttpStatusCode.InternalServerError,// unhandled error
                    },
                    KeyNotFoundException e => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError,
                };
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
