using System.Collections.Concurrent;

namespace Tests.Core.Xunit
{
	internal static class XunitRunState
	{
		public static ConcurrentBag<string> SeenDeprecations { get; } = new ConcurrentBag<string>();
	}
}
