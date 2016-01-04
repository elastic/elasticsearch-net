using System.Collections.Generic;
using System.Linq;
using Bogus;
using Nest;

namespace Tests.Framework.MockData
{
	public class Developer : Person
	{
		public string OnlineHandle { get; set; }
		public Gender Gender { get; set; }
		public string PrivateValue { get; set; }
		public string IPAddress { get; set; }

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
				.RuleFor(p => p.IPAddress, p => p.Internet.Ip())
			;

		public static IList<Developer> Developers { get; } =
			Developer.Generator.Generate(1000).ToList();
	}
}