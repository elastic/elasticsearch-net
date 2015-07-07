using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Nest;
using Newtonsoft.Json;

namespace Tests.Framework.MockData
{
	internal static class Gimme
	{
		static Gimme()
		{
			Randomizer.Seed = new Random(1337);
		}

		public static Randomizer Random = new Randomizer();
	}

	public class Project
	{
		public string Name { get; set; }
		public DateTime StartedOn { get; set; }
		public DateTime LastActivity { get; set; }
		public Developer LeadDeveloper { get; set; }
		public IEnumerable<Tag> Tags { get; set; }

		public static Faker<Project> Generator { get; } =
			new Faker<Project>()
				.RuleFor(p => p.Name, f => f.Person.Company.Name)
				.RuleFor(p => p.StartedOn, p => p.Date.Past())
				.RuleFor(p => p.LastActivity, p => p.Date.Recent())
				.RuleFor(p => p.LeadDeveloper, p => Developer.Developers[Gimme.Random.Number(0, Developer.Developers.Count -1)])
				.RuleFor(p => p.Tags, f => Tag.Generator.Generate(Gimme.Random.Number(1, 50)));

		public static IList<Project> Projects { get; } =
			Project.Generator.Generate(100).ToList();
	}

	public class Tag
	{
		public DateTime Added { get; set; }
		public string Name { get; set; }

		public static Faker<Tag> Generator { get; } =
			new Faker<Tag>()
				.RuleFor(p => p.Name, p => p.Lorem.Words(1).First())
				.RuleFor(p => p.Added, p => p.Date.Recent())
			;
	}


	public class CommitActivity
	{
		public string Id { get; set; }
		public string ProjectName { get; set; }
		public string Message { get; set; }
		public Developer Committer { get; set; }

		public static Faker<CommitActivity> Generator { get; } =
			new Faker<CommitActivity>()
				.RuleFor(p => p.Id, p => Guid.NewGuid().ToString("N").Substring(0, 8))
				.RuleFor(p => p.ProjectName, p => Project.Projects[Gimme.Random.Number(0, Project.Projects.Count -1)].Name)
				.RuleFor(p => p.Committer, p => Developer.Developers[Gimme.Random.Number(0, Developer.Developers.Count -1)])
				.RuleFor(p => p.Message, p => p.Lorem.Paragraph(Gimme.Random.Number(1, 3)))
			;
	}

	public enum Gender
	{
		Male, Female, NoneOfYourBeeswax
	}

	public class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string JobTitle { get; set; }
		public GeoLocation Location { get; set; }

		protected static int IdState = 0;

		public static Faker<Person> Generator { get; } =
			new Faker<Person>()
				.RuleFor(p => p.Id, p => IdState++)
				.RuleFor(p => p.FirstName, p => p.Name.FirstName())
				.RuleFor(p => p.LastName, p => p.Name.LastName())
				.RuleFor(p => p.JobTitle, p => p.Name.JobTitle())
				.RuleFor(p => p.Location, p => new GeoLocation(Gimme.Random.Number(-90, 90), Gimme.Random.Number(-180, 180)))
			;

		public static IList<Person> Persons { get; } =
			Person.Generator.Generate(1000).ToList();
	}

	public class Developer : Person
	{
		public string OnlineHandle { get; set; }
		public Gender Gender { get; set; }
		public string PrivateValue { get; set; }

		public new static Faker<Developer> Generator { get; } =
			new Faker<Developer>()
				.RuleFor(p => p.Id, p => IdState++)
				.RuleFor(p => p.FirstName, p => p.Name.FirstName())
				.RuleFor(p => p.LastName, p => p.Name.LastName())
				.RuleFor(p => p.JobTitle, p => p.Name.JobTitle())
				.RuleFor(p => p.Location, p => new GeoLocation(Gimme.Random.Number(-90, 90), Gimme.Random.Number(-180, 180)))
				.RuleFor(p => p.OnlineHandle, p => p.Internet.UserName())
				.RuleFor(p => p.Gender, p => p.PickRandom<Gender>())
				.RuleFor(p => p.PrivateValue, p => "THIS SHOULD NEVER BE INDEXED")
			;

		public static IList<Developer> Developers { get; } =
			Developer.Generator.Generate(1000).ToList();
	}

}
