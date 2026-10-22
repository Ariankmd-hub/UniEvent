namespace UniEvent.Application.DTOs.User
{
    public class CreateUserDto
    {

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string UniversityIdNumber { get; set; } = string.Empty;
    }
}
