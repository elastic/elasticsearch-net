using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using AutoPoco;
using Nest.TestData.Domain;
using AutoPoco.DataSources;
using AutoPoco.Configuration;
using ElasticSearch.Client;

namespace Nest.TestData
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = CreateClient();
            if (client.IsValid)
            {
                var projects = CreateProjectsData();
                client.Index(projects);

            }

            // Get a collection of users
        }

        public static ElasticClient CreateClient()
        {
            var settings = new ConnectionSettings(TestData.Default.Host, TestData.Default.Port)
                                        .SetDefaultIndex(TestData.Default.DefaultIndex)
                                        .SetMaximumAsyncConnections(TestData.Default.MaximumAsyncConnections);
            return new ElasticClient(settings);
        }

        public static IEnumerable<ElasticSearchProject> CreateProjectsData()
        {
            IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();

                });
                x.AddFromAssemblyContainingType<Person>();

                x.Include<Person>()
                    .Setup(c => c.Id).Use<IntegerIdSource>()
                   .Setup(c => c.Email).Use<EmailAddressSource>()
                   .Setup(c => c.FirstName).Use<FirstNameSource>()
                   .Setup(c => c.LastName).Use<LastNameSource>()
                   .Setup(c => c.DateOfBirth).Use<DateOfBirthSource>();
                x.Include<ElasticSearchProject>()
                    .Setup(c => c.Id).Use<IntegerIdSource>()
                  .Setup(c => c.Country).Use<CountrySource>()
                  .Setup(c => c.Name).Use<ElasticSearchProjectsDataSource>()
                  .Setup(c => c.Folllowers).Value(new List<Person>());
            });
            var session = factory.CreateSession();
            // Get a single user
            var count = ElasticSearchProjectsDataSource.Count();
            var users = session.List<Person>(100).Get();
            var projects = session.List<ElasticSearchProject>(count).Get();
            var i = 0;
            foreach (var p in projects)
            {


                var take = (int)100 / count;
                var skip = i * take;
                var followers = users.Skip(i * take).Take(take).ToList();
                p.Folllowers = followers;
                i++;
            }
            return projects;
        }
    }


}
