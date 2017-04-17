using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using System.Collections.Generic;
using System.Web.Http;
using WebApiDocumentation.HelpPage.Models;

namespace WebApiDocumentation.HelpPage.Controllers
{
    public class PersonController : ApiController
    {
        private readonly IGenerationSessionFactory _factory;

        public PersonController()
        {
            _factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });
                x.Include<PersonDetails>()
                    .Setup(s => s.FirstName).Use<FirstNameSource>()
                    .Setup(s => s.LastName).Use<LastNameSource>()
                    .Setup(s => s.EmailAddress).Use<EmailAddressSource>();
            });
        }

        // GET: api/Person
        public IList<PersonDetails> Get()
        {
            var session = _factory.CreateSession();
            return session.List<PersonDetails>(10).Get();
        }

        // GET: api/Person/5
        public Models.PersonDetails Get(int id)
        {
            var session = _factory.CreateSession();
            return session.Single<PersonDetails>().Get();
        }
    }
}
