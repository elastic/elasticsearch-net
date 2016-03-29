using System;
using System.Linq;
using Bogus;

namespace Tests.Framework.MockData
{
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
}