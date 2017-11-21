using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace Tests.Framework.MockData
{
	internal static class Gimme
	{
		public static Randomizer Random = new Randomizer();

		private static readonly object _lock = new object();

		/// <summary>
		/// Still hitting locking issues, whereas i though https://github.com/bchavez/Bogus/issues/46
		/// fixed it.
		/// </summary>
		public static T Lock<T>(Func<T> act)
		{
			lock (_lock)
			{
				return act();
			}
		}

		//TODO this is not lazy new bogus has GenerateLazy()
		public static IList<T> GenerateLocked<T>(this Faker<T> faker, int count) where T : class
		{
			return Gimme.Lock(() => faker.Generate(count)).ToList();
		}
		public static T GenerateLocked<T>(this Faker<T> faker) where T : class
		{
			return Gimme.Lock(() => faker.Generate());
		}
	}


}
