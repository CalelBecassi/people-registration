using PeopleRegistration.API.Entities;
using PeopleRegistration.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PeopleRegistration.API.Controllers
{
    [Route("api/peoples")]
    [ApiController]
    public class PeoplesController : ControllerBase
    {
        private readonly PeoplesDbContext _context;

        public PeoplesController(PeoplesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var peoples = _context.Peoples.Where(d => !d.EstaAtivo).ToList();

            return Ok(peoples);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var people = _context.Peoples.SingleOrDefault(d => d.Id == id);

            if (people == null)
            {
                return NotFound();
            }
             
            return Ok(people);
        }

        [HttpPost]
        public IActionResult Post(People people)
        {
            _context.Peoples.Add(people);

            return CreatedAtAction(nameof(GetById), new { id = people.Id }, people);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, People input)
        {
            var people = _context.Peoples.SingleOrDefault(d => d.Id == id);

            if (people == null)
            {
                return NotFound();
            }

            people.Update(input.Name, input.Cpf, input.Nascimento, input.EstaAtivo);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var people = _context.Peoples.SingleOrDefault(d => d.Id == id);

            if (people == null)
            {
                return NotFound();
            }

            people.Delete();

            return NoContent();
        }

        [HttpPost("{id}/phones")]
        public IActionResult PostPhone(Guid id, PeoplePhone phone)
        {
            var people = _context.Peoples.SingleOrDefault(d => d.Id == id);

            if (people == null)
            {
                return NotFound();
            }

            people.Phones.Add(phone);

            return NoContent();
        }
    }
}
