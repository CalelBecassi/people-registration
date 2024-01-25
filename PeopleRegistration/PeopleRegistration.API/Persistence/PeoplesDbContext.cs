using PeopleRegistration.API.Entities;

namespace PeopleRegistration.API.Persistence
{
    public class PeoplesDbContext
    {
        public List<People> Peoples { get; set; }

        public PeoplesDbContext()
        {
            Peoples = new List<People>();
        }
    }
}
