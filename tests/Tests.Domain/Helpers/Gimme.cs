using Bogus;
using Tests.Configuration;

namespace Tests.Domain.Helpers
{
	public static class Gimme
	{
		public static Randomizer Random { get; } = new Randomizer(TestConfiguration.Instance.Seed);
	}
}
