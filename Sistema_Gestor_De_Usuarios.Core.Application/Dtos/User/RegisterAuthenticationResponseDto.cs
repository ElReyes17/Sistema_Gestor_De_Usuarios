


using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.Phone;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Dtos.User
{
    public class RegisterAuthenticationResponseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }     
        public ICollection<PhoneDto> Phones { get; set; }
    }
}
