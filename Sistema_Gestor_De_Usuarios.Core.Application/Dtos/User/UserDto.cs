using Sistema_Gestor_De_Usuarios.Core.Application.Dtos.Phone;

namespace Sistema_Gestor_De_Usuarios.Core.Application.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<PhoneDto> Phones { get; set; }
    }
}
