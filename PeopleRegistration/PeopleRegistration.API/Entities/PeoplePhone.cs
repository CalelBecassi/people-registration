namespace PeopleRegistration.API.Entities
{
    public class PeoplePhone
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Number { get; set; }

        public Guid PeopleId { get; set; }
    }
}