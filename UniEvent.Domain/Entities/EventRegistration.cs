namespace UniEvent.Domain.Entities
{
    public class EventRegistration
    {
        // primary key 

        public int Id { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public DateTime RegistrationDate { get; set; }

        // Navigation Property 
        public Event Event { get; set; } = null!; 

        public User User { get; set; } = null!;

    }
}
