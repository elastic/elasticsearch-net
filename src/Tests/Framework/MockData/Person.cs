using System.Collections.Generic;
using System.Linq;
using Bogus;
using Nest;

namespace Tests.Framework.MockData
{
	public class Person
	{
		public long Id { get; set; }
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
}