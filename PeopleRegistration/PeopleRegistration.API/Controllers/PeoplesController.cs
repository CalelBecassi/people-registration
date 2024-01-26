using PeopleRegistration.API.Entities;
using PeopleRegistration.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var peoples = _context.Peoples
                .Include(p => p.Phones)
                .Where(p => p.EstaAtivo)
                .ToList();

            return Ok(peoples);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var people = _context.Peoples
                .Include(p => p.Phones)
                .SingleOrDefault(p => p.Id == id);

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
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = people.Id }, people);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, People input)
        {
            var people = _context.Peoples.SingleOrDefault(p => p.Id == id);

            if (people == null)
            {
                return NotFound();
            }

            people.Update(input.Name, input.Cpf, input.Nascimento, input.EstaAtivo);

            _context.Peoples.Update(people);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var people = _context.Peoples.SingleOrDefault(p => p.Id == id);

            if (people == null)
            {
                return NotFound();
            }

            people.Delete();

            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id}/phones")]
        public IActionResult PostPhone(Guid id, PeoplePhone phone)
        {
            phone.PeopleId = id;
            var people = _context.Peoples.Any(p => p.Id == id);

            if (!people)
            {
                return NotFound();
            }

            _context.PeoplePhones.Add(phone);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
