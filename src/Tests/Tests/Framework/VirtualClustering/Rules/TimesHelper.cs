using System;

namespace Tests.Framework
{
	public static class TimesHelper
	{
		public class AllTimes { internal AllTimes() { } }

		public static AllTimes Always = new AllTimes();
		public static readonly int Once = 0;
		public static readonly int Twice = 1;
		public static int Times(int n) => Math.Max(0, n - 1);
	}
}