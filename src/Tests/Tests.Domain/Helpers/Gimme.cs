using Bogus;
using Tests.Framework;

namespace Tests.Domain.Helpers
{
	internal static class Gimme
	{
		public static Randomizer Random { get; } = new Randomizer(TestConfiguration.Instance.Seed);
	}
}
