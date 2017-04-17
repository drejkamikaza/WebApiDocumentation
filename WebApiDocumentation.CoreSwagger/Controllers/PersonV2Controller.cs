using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiDocumentation.CoreSwagger.Models;

namespace WebApiDocumentation.CoreSwagger.Controllers
{
    /// <summary>
    /// Version 2 Person Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/person")]
    [ApiVersion("2.0")]
    public class PersonV2Controller : Controller
    {
        /// <summary>
        /// Version 2 get persons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PersonDetails> Get()
        {
            return new List<PersonDetails> { new PersonDetails { FirstName = "Version 2 Person", LastName = "Version 2 Last Name", EmailAddress = "version2@example.com" } };
        }

        /// <summary>
        /// Version 2 get single person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public PersonDetails Get(int id)
        {
            return new PersonDetails { FirstName = "Version 2 SINGLE", LastName = "Version 2 Last Name", EmailAddress = "version2@example.com" };
        }
    }
}
