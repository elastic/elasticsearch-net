using System;
using System.Linq;
using Bogus;
using Tests.Configuration;

namespace Tests.Domain
{
	public class Tag
	{
		public DateTime Added { get; set; }
		public string Name { get; set; }

		public static Faker<Tag> Generator { get; } =
			new Faker<Tag>()
				.UseSeed(TestConfiguration.Instance.Seed)
				.RuleFor(p => p.Name, p => p.Lorem.Words(1).First())
				.RuleFor(p => p.Added, p => p.Date.Recent())
			.Clone();
	}
}
