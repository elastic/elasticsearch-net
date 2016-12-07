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

		private static readonly object _lock = new object();

		/// <summary>
		/// Temporarily do our own locking around the generator until https://github.com/bchavez/Bogus/issues/46
		/// is confirmed resolved
		/// </summary>
		public static T Lock<T>(Func<T> act)
		{
			lock (_lock)
			{
				return act();
			}
		}
	}
}