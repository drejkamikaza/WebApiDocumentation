using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiDocumentation.CoreSwagger.Models;

namespace WebApiDocumentation.CoreSwagger.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<PersonDetails> Get()
        {
            var data = new List<PersonDetails>
            {
                new PersonDetails { FirstName = "John", LastName = "Doe 1", EmailAddress = "1111@example.com" },
                new PersonDetails { FirstName = "John", LastName = "Doe 2", EmailAddress = "2222@example.com" },
                new PersonDetails { FirstName = "John", LastName = "Doe 3", EmailAddress = "3333@example.com" }
            };
            return data;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public PersonDetails Get(int id)
        {
            return new PersonDetails { FirstName = "John", LastName = "Doe 1", EmailAddress = "1111@example.com" };
        }
    }
}
