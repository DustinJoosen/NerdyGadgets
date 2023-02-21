namespace NerdyGadgets.Dtos
{
    public class AuthDto
    {
        public AuthDto()
        {

        }
        public AuthDto(string email, string password) 
        {
            this.EmailAddress = email;
            this.Password = password;
        }

        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
