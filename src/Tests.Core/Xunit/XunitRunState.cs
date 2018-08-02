using System.Collections.Concurrent;

namespace Tests.Framework.ManagedElasticsearch
{
	internal static class XunitRunState
	{
		public static ConcurrentBag<string> SeenDeprecations { get; } = new ConcurrentBag<string>();
	}
}
