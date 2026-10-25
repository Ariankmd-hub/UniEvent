namespace UniEvent.Domain.Entities
{
    public class User
    {
        // primary key 
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber {  get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string UniversityIdNumber { get; set; } = string.Empty;
    }
}
