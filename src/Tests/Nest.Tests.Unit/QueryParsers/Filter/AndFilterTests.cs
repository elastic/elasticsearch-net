using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class AndFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void And_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var andFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.And,
				f=>f.And(Filter1, Filter2)
				);
			andFilter.Filters.Should().NotBeEmpty().And.HaveCount(2);

			AssertIsTermFilter(this.Filter1, andFilter.Filters.First().Term);
			AssertIsTermFilter(this.Filter2, andFilter.Filters.Last().Term);
		}

	}
}