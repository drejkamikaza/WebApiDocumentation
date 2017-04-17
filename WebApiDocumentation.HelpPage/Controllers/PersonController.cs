using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using System.Collections.Generic;
using System.Web.Http;
using WebApiDocumentation.HelpPage.Models;

namespace WebApiDocumentation.HelpPage.Controllers
{
    /// <summary>
    /// Person details controller
    /// </summary>
    public class PersonController : ApiController
    {
        private readonly IGenerationSessionFactory _factory;

        /// <summary>
        /// Create new Person default ctor
        /// </summary>
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
        /// Get list of Persons from local store
        /// </summary>
        /// <returns>Returns empty list or all persons</returns>
        public IList<PersonDetails> Get()
        {
            var session = _factory.CreateSession();
            return session.List<PersonDetails>(10).Get();
        }

        /// <summary>
        /// Get single person
        /// </summary>
        /// <param name="id">Person ID</param>
        /// <returns>Returns person details or null</returns>
        public Models.PersonDetails Get(int id)
        {
            var session = _factory.CreateSession();
            return session.Single<PersonDetails>().Get();
        }
    }
}
