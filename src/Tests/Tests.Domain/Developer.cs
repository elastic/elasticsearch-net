using System.Collections.Generic;
using System.Threading;
using Bogus;
using Nest;
using Tests.Configuration;
using Tests.Domain.Helpers;

namespace Tests.Domain
{
	public class Developer : Person
	{
		public string OnlineHandle { get; set; }
		public Gender Gender { get; set; }
		public string PrivateValue { get; set; }
		public string IpAddress { get; set; }

		// not populated by generator. Used by ingest geoip test
		public GeoIp GeoIp { get; set; }

		public new static Faker<Developer> Generator { get; } =
			new Faker<Developer>()
				.UseSeed(TestConfiguration.Instance.Seed)
				.RuleFor(p => p.Id, p => Interlocked.Increment(ref IdState))
				.RuleFor(p => p.FirstName, p => p.Name.FirstName())
				.RuleFor(p => p.LastName, p => p.Name.LastName())
				.RuleFor(p => p.JobTitle, p => p.Name.JobTitle())
				.RuleFor(p => p.Location, p => new GeoLocation(Gimme.Random.Number(-90, 90), Gimme.Random.Number(-180, 180)))
				.RuleFor(p => p.OnlineHandle, p => p.Internet.UserName())
				.RuleFor(p => p.Gender, p => p.PickRandom<Gender>())
				.RuleFor(p => p.PrivateValue, p => "THIS SHOULD NEVER BE INDEXED")
				.RuleFor(p => p.IpAddress, p => p.Internet.Ip())
			;

		public static IList<Developer> Developers { get; } = Developer.Generator.Clone().Generate(1000);
	}
}
