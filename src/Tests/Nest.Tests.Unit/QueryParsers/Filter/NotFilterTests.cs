using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class NotFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Not_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var notFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Not,
				f=>f.Not(ff => Filter1)
				);
			AssertIsTermFilter(Filter1, notFilter.Filter.Term);
		}
		
	}
}