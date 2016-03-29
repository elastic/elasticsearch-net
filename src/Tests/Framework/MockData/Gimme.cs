using System;
using Bogus;

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
}