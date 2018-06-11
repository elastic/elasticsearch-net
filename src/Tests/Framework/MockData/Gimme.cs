using Bogus;

namespace Tests.Framework.MockData
{
	internal static class Gimme
	{
		public static Randomizer Random { get; } = new Randomizer(TestClient.Configuration.Seed);
	}
}
