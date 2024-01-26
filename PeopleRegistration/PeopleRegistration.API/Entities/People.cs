namespace PeopleRegistration.API.Entities
{
    public class People
    {
        public People()
        {
            Phones = new List<PeoplePhone> { };
            EstaAtivo = true;
        }
        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public DateTime Nascimento { get; set; }

        public bool EstaAtivo { get; set; }

        public List<PeoplePhone> Phones { get; set; }

        public void Update(string name, string cpf, DateTime nascimento, bool estaAtivo)
        {
            Name = name;
            Cpf = cpf;
            Nascimento = nascimento;
            EstaAtivo = estaAtivo;
        }

        public void Delete()
        {
            EstaAtivo = false;
        }
    }
}
