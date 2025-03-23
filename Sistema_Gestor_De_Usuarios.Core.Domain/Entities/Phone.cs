

namespace Sistema_Gestor_De_Usuarios.Core.Domain.Entities
{

    public class Phone
    {
        public int PhoneId { get; set; }
        public string Number { get; set; } = string.Empty;
        public string CityCode { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;


        //Relationships
        public Guid UserId { get; set; }
        public User User { get; set; } 


    }
}
