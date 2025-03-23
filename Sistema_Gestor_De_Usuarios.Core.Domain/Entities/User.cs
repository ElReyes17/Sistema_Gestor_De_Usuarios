
namespace Sistema_Gestor_De_Usuarios.Core.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } 
        public DateTime? Modified { get; set; }
        public DateTime? LastLogin { get; set; }     
        public bool IsActive { get; set; }

        //Relationships
        public ICollection<Phone> Phones { get; set; } = new List<Phone>();


    }
}
