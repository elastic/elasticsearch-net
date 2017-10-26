using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tests.Framework.MockData
{
	public class Project
	{
		public JoinField Join => JoinField.Root<Project>();
		public string Name { get; set; }
		public string Description { get; set; }
		public StateOfBeing State { get; set; }
		public DateTime StartedOn { get; set; }
		public string DateString { get; set; }
		public DateTime LastActivity { get; set; }
		public Developer LeadDeveloper { get; set; }
		public IEnumerable<Tag> Tags { get; set; }
		public IList<Tag> CuratedTags { get; set; }
		public Dictionary<string, Metadata> Metadata { get; set; }
		public SimpleGeoPoint Location { get; set; }
		public int? NumberOfCommits { get; set; }
		public int? NumberOfContributors { get; set; }
		public CompletionField Suggest { get; set; }
		public IEnumerable<string> Branches { get; set; }
		public Ranges Ranges { get; set; }
		[JsonIgnore]
		public string NotWrittenByDefaultSerializer { get; set; }
		[JsonIgnore]
		public string NotReadByDefaultSerializer { get; set; }

		public static Faker<Project> Generator { get; } =
			new Faker<Project>()
				.RuleFor(p => p.Name, f => f.Person.Company.Name)
				.RuleFor(p => p.Description, f => f.Lorem.Paragraphs(3))
				.RuleFor(p => p.State, f => f.PickRandom<StateOfBeing>())
				.RuleFor(p => p.StartedOn, p => p.Date.Past())
				.RuleFor(p => p.DateString, (p, d) => d.StartedOn.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"))
				.RuleFor(p => p.LastActivity, p => p.Date.Recent())
				.RuleFor(p => p.LeadDeveloper, p => Developer.Developers[Gimme.Random.Number(0, Developer.Developers.Count -1)])
				.RuleFor(p => p.Tags, f => Tag.Generator.Generate(Gimme.Random.Number(2, 50)))
				.RuleFor(p => p.CuratedTags, f => Tag.Generator.Generate(Gimme.Random.Number(1, 5)).ToList())
				.RuleFor(p => p.Location, f => SimpleGeoPoint.Generator.Generate())
				.RuleFor(p => p.NumberOfCommits, f => Gimme.Random.Number(1, 1000))
				.RuleFor(p => p.NumberOfContributors, f => Gimme.Random.Number(1, 200))
				.RuleFor(p => p.Ranges, f => Ranges.Generator.Generate())
				.RuleFor(p => p.Suggest, f => new CompletionField
					{
						Input = new[] { f.Person.Company.Name },
						Contexts = new Dictionary<string, IEnumerable<string>>
						{
							{ "color", new [] { "red", "blue", "green", "violet", "yellow" }.Take(Gimme.Random.Number(1, 4)) }
						}
				}
				)
			;

		public static IList<Project> Projects { get; } = Project.Generator.Generate(100).ToList();

	    public static Project First { get; } = Projects.First();

		public static readonly Project Instance = new Project
		{
			Name = Projects.First().Name,
			LeadDeveloper = new Developer() { FirstName = "Martijn", LastName = "Laarman" },
			StartedOn = new DateTime(2015, 1, 1),
			DateString = new DateTime(2015, 1, 1).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"),
			Location = new SimpleGeoPoint { Lat = 42.1523, Lon = -80.321 }
		};

		public static object InstanceAnonymous => TestClient.Configuration.UsingCustomSourceSerializer
			? InstanceAnonymousSourceSerializer
			: InstanceAnonymousDefault;

		private static readonly object InstanceAnonymousDefault = new
		{
			name = Projects.First().Name,
			join = Instance.Join,
			state = "BellyUp",
			startedOn = "2015-01-01T00:00:00",
			lastActivity = "0001-01-01T00:00:00",
			dateString = new DateTime(2015, 1, 1).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"),
			leadDeveloper = new { gender = "Male", id = 0, firstName = "Martijn", lastName = "Laarman" },
			location = new { lat = Instance.Location.Lat, lon = Instance.Location.Lon }
		};
		private static readonly object InstanceAnonymousSourceSerializer = new
		{
			name = Projects.First().Name,
			join = Instance.Join,
			notWrittenByDefaultSerializer = "written",
			state = "BellyUp",
			startedOn = "2015-01-01T00:00:00",
			lastActivity = "0001-01-01T00:00:00",
			dateString = new DateTime(2015, 1, 1).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"),
			leadDeveloper = new { gender = "Male", id = 0, firstName = "Martijn", lastName = "Laarman" },
			location = new { lat = Instance.Location.Lat, lon = Instance.Location.Lon }
		};
	}

	public class SimpleGeoPoint
	{
		public double Lat { get; set; }
		public double Lon { get; set; }

		public static Faker<SimpleGeoPoint> Generator { get; } =
			new Faker<SimpleGeoPoint>()
				.RuleFor(p => p.Lat, f => f.Address.Latitude())
				.RuleFor(p => p.Lon, f => f.Address.Longitude())
			;
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum StateOfBeing
	{
		BellyUp,
		Stable,
		VeryActive
	}

	public class Metadata
	{
		public DateTime Created { get; set; }
	}

	public class ProjectPayload
	{
		public string Name { get; set; }
		public StateOfBeing? State { get; set; }
	}
}
