using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace Tests.Framework.MockData
{
	internal static class Gimme
	{
		public static Randomizer Random = new Randomizer(TestClient.Configuration.Seed);
	}
}
