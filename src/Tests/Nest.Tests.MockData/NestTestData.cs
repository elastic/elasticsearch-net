using System;
using System.Collections.Generic;
using System.Linq;
using AutoPoco.Engine;
using AutoPoco;
using Nest.Tests.MockData.Domain;
using AutoPoco.DataSources;
using Nest.Tests.MockData.DataSources;

namespace Nest.Tests.MockData
{
	public static class NestTestData
	{
		private static IList<Person> _People { get; set; }
		private static IList<BoolTerm> _BoolTerms { get; set; }
		private static IList<ElasticsearchProject> _Data { get; set; }
		private static IList<Parent> _Parents { get; set; }
		private static IList<Child> _Children { get; set; }
		public static IGenerationSession _Session { get; set; }
		public static IGenerationSession Session
		{
			get
			{
				if (_Session != null)
					return _Session;

				IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
				{
					x.Conventions(c =>
					{
						c.UseDefaultConventions();

					});
					x.AddFromAssemblyContainingType<Person>();
					x.Include<CustomGeoLocation>()
						.Setup(c => c.lat).Use<FloatSource>()
						.Setup(c => c.lon).Use<FloatSource>();
					x.Include<BoolTerm>()
						.Setup(c => c.Name1).Use<IncrementalNameSource>("a")
						.Setup(c => c.Name2).Use<IncrementalNameSource>("b");
					x.Include<Person>()
						.Setup(c => c.Id).Use<IntegerIdSource>()
						.Setup(c => c.Email).Use<EmailAddressSource>()
						.Setup(c => c.FirstName).Use<FirstNameSource>()
						.Setup(c => c.LastName).Use<LastNameSource>()
						.Setup(c => c.DateOfBirth).Use<DateOfBirthSource>()
						.Setup(c => c.Age).Use<AgeSource>()
						.Setup(c => c.PlaceOfBirth).Use<GeoLocationSource>();
					x.Include<ElasticsearchProject>()
						.Setup(c => c.Id).Use<IntegerIdSource>()
						.Setup(c => c.LongValue).Use<LongSource>()
						.Setup(c => c.FloatValue).Use<FloatSource>()
						.Setup(c => c.DoubleValue).Use<DoubleSource>()
						.Setup(c => c.BoolValue).Use<BoolSource>()
						.Setup(c => c.IntValues).Use<IntListSource>()
						.Setup(c => c.FloatValues).Use<FloatArraySource>()
						.Setup(c => c.LOC).Use<LOCSource>()
						.Setup(c => c.PingIP).Use<IpSource>()
						.Setup(c => c.Country).Use<CountrySource>()
						.Setup(c => c.Origin).Use<GeoLocationSource>()
						.Setup(c => c.Name).Use<ElasticsearchProjectsDataSource>()
						.Setup(c => c.Followers).Value(new List<Person>())
						.Setup(c => c.Contributors).Value(new List<Person>())
						.Setup(c => c.StartedOn).Use<DateOfBirthSource>()
						.Setup(c => c.Content).Use<ElasticsearchProjectDescriptionSource>();
					x.Include<Parent>()
						.Setup(c => c.Id).Use<IntegerIdSource>()
						.Setup(c => c.ParentName).Use<RandomStringSource>(1, 50);
					x.Include<Child>()
						.Setup(c => c.Id).Use<IntegerIdSource>()
						.Setup(c => c.ChildName).Use<RandomStringSource>(1, 50);
				});
				_Session = factory.CreateSession();
				return _Session;
			}
		}

		public static IList<Person> People
		{
			get
			{
				if (_People == null)
					_People = Session.List<Person>(100).Get();
				return _People;
			}
		}

		public static IList<BoolTerm> BoolTerms
		{
			get
			{
				if (_BoolTerms == null)
					_BoolTerms = Session.List<BoolTerm>(100).Get();
				return _BoolTerms;

			}
		}

		public static IList<Parent> Parents
		{
			get
			{
				if (_Parents == null)
					_Parents = Session.List<Parent>(100).Get();
				return _Parents;
			}
		}

		public static IList<Child> Children
		{
			get
			{
				if (_Children == null)
					_Children = Session.List<Child>(100).Get();
				return _Children;
			}
		}
		public static IList<ElasticsearchProject> Data
		{
			get
			{
				if (_Data == null)
				{
					// Get a single user
					var count = ElasticsearchProjectsDataSource.Count();
					var users = Session.List<Person>(100).Get();
					var contributors = Session.List<Person>(100).Get();
					_Data = Session.List<ElasticsearchProject>(count).Get();
					var i = 0;
					foreach (var p in _Data)
					{
						var take = (int)100 / count;
						var skip = i * take;
						var followers = users.Skip(i * take).Take(take).ToList();
						var cont = contributors.Skip(i * take).Take(take).ToList();
						p.Contributors = cont;
						p.Followers = followers;
						i++;
					}
				}
				return _Data;
			}
		}
	}
}
