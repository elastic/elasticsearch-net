using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Tests.Configuration;
using Tests.Domain.Extensions;
using Tests.Domain.Helpers;

namespace Tests.Domain
{
	public class Project
	{
		public JoinField Join => JoinField.Root<Project>();
		public string Name { get; set; }
		public string Description { get; set; }
		public StateOfBeing State { get; set; }

        //the first applies when using internal source serializer the latter when using JsonNetSourceSerializer
        [StringEnum, JsonConverter(typeof(StringEnumConverter))]
		public Visibility Visibility { get; set; }
		public DateTime StartedOn { get; set; }
		public string DateString { get; set; }
		public DateTime LastActivity { get; set; }
		public Developer LeadDeveloper { get; set; }
		public IEnumerable<Tag> Tags { get; set; }
		public IList<Tag> CuratedTags { get; set; }
		public Dictionary<string, Metadata> Metadata { get; set; }
		public SimpleGeoPoint Location { get; set; }
		public int? NumberOfCommits { get; set; }
		public int NumberOfContributors { get; set; }
		public CompletionField Suggest { get; set; }
		public IEnumerable<string> Branches { get; set; }
		public Ranges Ranges { get; set; }
		public int? RequiredBranches => this.Branches?.Count();

		public SourceOnlyObject SourceOnly { get; set; }

		public static Faker<Project> Generator { get; } =
			new Faker<Project>()
				.UseSeed(TestConfiguration.Instance.Seed)
				.RuleFor(p => p.Name, f => f.Person.Company.Name + f.UniqueIndex.ToString())
				.RuleFor(p => p.Description, f => f.Lorem.Paragraphs(3))
				.RuleFor(p => p.State, f => f.PickRandom<StateOfBeing>())
				.RuleFor(p => p.Visibility, f => f.PickRandom<Visibility>())
				.RuleFor(p => p.StartedOn, p => p.Date.Past())
				.RuleFor(p => p.DateString, (p, d) => d.StartedOn.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"))
				.RuleFor(p => p.LastActivity, p => p.Date.Recent())
				.RuleFor(p => p.LeadDeveloper, p => Developer.Developers[Gimme.Random.Number(0, Developer.Developers.Count -1)])
				.RuleFor(p => p.Tags, f => Tag.Generator.Generate(Gimme.Random.Number(2, 50)))
				.RuleFor(p => p.CuratedTags, f => Tag.Generator.Generate(Gimme.Random.Number(1, 5)))
				.RuleFor(p => p.Location, f => SimpleGeoPoint.Generator.Generate())
				.RuleFor(p => p.NumberOfCommits, f => Gimme.Random.Number(1, 1000))
				.RuleFor(p => p.NumberOfContributors, f => Gimme.Random.Number(1, 200))
				.RuleFor(p => p.Ranges, f => Ranges.Generator.Generate())
				.RuleFor(p => p.Branches, f => Gimme.Random.ListItems(new List<string> { "master", "dev", "release", "qa", "test" }, 2))
				.RuleFor(p => p.SourceOnly, f =>
					TestConfiguration.Instance.Random.SourceSerializer ? new SourceOnlyObject() : null
				)
				.RuleFor(p => p.Suggest, f => new CompletionField
				{
					Input = new[] { f.Person.Company.Name },
					Contexts = new Dictionary<string, IEnumerable<string>>
					{
						{ "color", new [] { "red", "blue", "green", "violet", "yellow" }.Take(Gimme.Random.Number(1, 4)) }
					}
				})
			;

		public static IList<Project> Projects { get; } = Project.Generator.Clone().Generate(100);

	    public static Project First { get; } = Projects.First();

		public static readonly Project Instance = new Project
		{
			Name = Projects.First().Name,
			LeadDeveloper = new Developer() { FirstName = "Martijn", LastName = "Laarman" },
			StartedOn = new DateTime(2015, 1, 1),
			DateString = new DateTime(2015, 1, 1).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"),
			Location = new SimpleGeoPoint { Lat = 42.1523, Lon = -80.321 },
			SourceOnly = TestConfiguration.Instance.Random.SourceSerializer ? new SourceOnlyObject() : null
		};

		public static readonly string Routing = Project.Instance.Name;

		public static object InstanceAnonymous => TestConfiguration.Instance.Random.SourceSerializer
			? InstanceAnonymousSourceSerializer
			: InstanceAnonymousDefault;

		private static readonly object InstanceAnonymousDefault = new
		{
			name = Projects.First().Name,
			join = Instance.Join.ToAnonymousObject(),
			state = "BellyUp",
			visibility = "Public",
			startedOn = "2015-01-01T00:00:00",
			lastActivity = "0001-01-01T00:00:00",
			numberOfContributors = 0,
			dateString = new DateTime(2015, 1, 1).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"),
			leadDeveloper = new { gender = "Male", id = 0, firstName = "Martijn", lastName = "Laarman" },
			location = new { lat = Instance.Location.Lat, lon = Instance.Location.Lon }
		};
		private static readonly object InstanceAnonymousSourceSerializer = new
		{
			name = Projects.First().Name,
			join = Instance.Join.ToAnonymousObject(),
			state = "BellyUp",
			visibility = "Public",
			startedOn = "2015-01-01T00:00:00",
			lastActivity = "0001-01-01T00:00:00",
			numberOfContributors = 0,
			dateString = new DateTime(2015, 1, 1).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"),
			leadDeveloper = new { gender = "Male", id = 0, firstName = "Martijn", lastName = "Laarman" },
			location = new { lat = Instance.Location.Lat, lon = Instance.Location.Lon },
			sourceOnly = new { notWrittenByDefaultSerializer = "written" }
		};
	}

	//the first applies when using internal source serializer the latter when using JsonNetSourceSerializer
	[StringEnum, JsonConverter(typeof(StringEnumConverter))]
	public enum StateOfBeing
	{
		BellyUp,
		Stable,
		VeryActive
	}

	public enum Visibility
	{
		Public,
		Private
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
