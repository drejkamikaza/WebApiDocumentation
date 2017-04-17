using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using System.Collections.Generic;
using System.Web.Http;
using WebApiDocumentation.AspNetSwagger.Models;

namespace WebApiDocumentation.AspNetSwagger.Controllers
{
    /// <summary>
    /// Person resource
    /// </summary>
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

        /// <summary>
        /// Get list on swagger
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PersonDetails> Get()
        {
            var session = _factory.CreateSession();
            return session.List<PersonDetails>(10).Get();
        }

        /// <summary>
        /// Get single person for swagger
        /// </summary>
        /// <param name="id">ID ID</param>
        /// <returns>Returns something</returns>
        public PersonDetails Get(int id)
        {
            var session = _factory.CreateSession();
            return session.Single<PersonDetails>().Get();
        }
    }
}
