using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using AutoPoco;
using Nest.TestData.Domain;
using AutoPoco.DataSources;
using AutoPoco.Configuration;

namespace Nest.TestData
{
	public static class NestTestData
	{
		private static IEnumerable<ElasticSearchProject> _Data { get; set; }
		public static IEnumerable<ElasticSearchProject> Data
		{
			get
			{
				if (_Data == null)
				{
					IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
					{
						x.Conventions(c =>
						{
							c.UseDefaultConventions();

						});
						x.AddFromAssemblyContainingType<Person>();
						x.Include<GeoLocation>()
							.Setup(c => c.lat).Use<FloatSource>()
							.Setup(c => c.lon).Use<FloatSource>();
						x.Include<Person>()
							.Setup(c => c.Id).Use<IntegerIdSource>()
							.Setup(c => c.Email).Use<EmailAddressSource>()
							.Setup(c => c.FirstName).Use<FirstNameSource>()
							.Setup(c => c.LastName).Use<LastNameSource>()
							.Setup(c => c.DateOfBirth).Use<DateOfBirthSource>()
							.Setup(c => c.PlaceOfBirth).Use <GeoLocationSource>();
						x.Include<ElasticSearchProject>()
							.Setup(c => c.Id).Use<IntegerIdSource>()
							.Setup(c => c.LongValue).Use<LongSource>()
							.Setup(c => c.FloatValue).Use<FloatSource>()
							.Setup(c => c.DoubleValue).Use<DoubleSource>()
							.Setup(c => c.LOC).Use<LOCSource>()
							.Setup(c => c.Country).Use<CountrySource>()
							.Setup(c => c.Origin).Use<GeoLocationSource>()
							.Setup(c => c.Name).Use<ElasticSearchProjectsDataSource>()
							.Setup(c => c.Followers).Value(new List<Person>())
							.Setup(c => c.StartedOn).Use<DateOfBirthSource>()
							.Setup(c => c.Content).Use<ElasticSearchProjectDescriptionSource>();
					});
					var session = factory.CreateSession();
					// Get a single user
					var count = ElasticSearchProjectsDataSource.Count();
					var users = session.List<Person>(100).Get();
					_Data = session.List<ElasticSearchProject>(count).Get();
					var i = 0;
					foreach (var p in _Data)
					{
						var take = (int)100 / count;
						var skip = i * take;
						var followers = users.Skip(i * take).Take(take).ToList();
						p.Followers = followers;
						i++;
					}
				}
				return _Data;
			}
		}
	}
}
