using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Bogus;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tests.Framework.MockData
{
	public class Project
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public StateOfBeing State { get; set; }
		public DateTime StartedOn { get; set; }
		public DateTime LastActivity { get; set; }
		public Developer LeadDeveloper { get; set; }
		public IEnumerable<Tag> Tags { get; set; }
		public IList<Tag> CuratedTags { get; set; }
		public Dictionary<string, Metadata> Metadata { get; set; }
		public SimpleGeoPoint Location { get; set; }

		public static Faker<Project> Generator { get; } =
			new Faker<Project>()
				.RuleFor(p => p.Name, f => f.Person.Company.Name)
				.RuleFor(p => p.Description, f => f.Lorem.Paragraphs(3))
				.RuleFor(p => p.State, f => f.PickRandom<StateOfBeing>())
				.RuleFor(p => p.StartedOn, p => p.Date.Past())
				.RuleFor(p => p.LastActivity, p => p.Date.Recent())
				.RuleFor(p => p.LeadDeveloper, p => Developer.Developers[Gimme.Random.Number(0, Developer.Developers.Count -1)])
				.RuleFor(p => p.Tags, f => Tag.Generator.Generate(Gimme.Random.Number(2, 50)))
				.RuleFor(p => p.CuratedTags, f => Tag.Generator.Generate(Gimme.Random.Number(1, 5)).ToList())
				.RuleFor(p => p.Location, f => SimpleGeoPoint.Generator.Generate())
			;

		public static IList<Project> Projects { get; } =
			Project.Generator.Generate(100).ToList();

		public static Project Instance = new Project
		{
			Name = Projects.First().Name,
			LeadDeveloper = new Developer() { FirstName = "Martijn", LastName = "Laarman" },
			StartedOn = new DateTime(2015, 1, 1),
			Location = new SimpleGeoPoint { Coordinates = new [] { 42.1523, -80.321} }
		};

		public static object InstanceAnonymous = new
		{
			name = Projects.First().Name,
			state = "BellyUp",
			startedOn = "2015-01-01T00:00:00",
			lastActivity = "0001-01-01T00:00:00",
			leadDeveloper = new {gender = "Male", id = 0, firstName = "Martijn", lastName = "Laarman"},
			location = new {type="point", coordinates= Project.Instance.Location.Coordinates }
		};
	}

	public class SimpleGeoPoint
	{
		public string Type => "point";
		public double[] Coordinates { get; set; }

		public static Faker<SimpleGeoPoint> Generator { get; } =
			new Faker<SimpleGeoPoint>()
				.RuleFor(p=>p.Coordinates, f => new [] { f.Address.Latitude(), f.Address.Latitude() });
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
}
