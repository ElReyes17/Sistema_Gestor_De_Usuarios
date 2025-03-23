

namespace Sistema_Gestor_De_Usuarios.Core.Application.Common
{
    public class ApiException(string message, int errorCode) : Exception(message)
    {
        public int ErrorCode { get; } = errorCode;
    }
}
