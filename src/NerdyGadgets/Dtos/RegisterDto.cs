namespace NerdyGadgets.Dtos
{
    public class RegisterDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string FirstName { get; set; }
        public string? Preposition { get; set; }
        public string LastName { get; set; }
    }
}
