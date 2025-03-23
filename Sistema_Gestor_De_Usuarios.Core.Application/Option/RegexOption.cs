namespace Sistema_Gestor_De_Usuarios.Options
{
    public class RegexOption
    {
        //private string PasswordRegex = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$";
        //private string PhoneRegex = @"^\+?\d{1,4}?[-.\s]?\(?\d+\)?[-.\s]?\d+[-.\s]?\d+$";
        //private string CityCode = @"^\d+$";
        //private string CountryCode = @"^\d{1,4}$";

        public string PasswordRegex { get; set; } = string.Empty;
        public string PhoneRegex { get; set; } = string.Empty;
        public string CityCode { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;

    }
}
