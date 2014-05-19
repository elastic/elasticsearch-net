using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class OrFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Or_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var orFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Or,
				f=>f.Or(Filter1, Filter2)
				);

			orFilter.Filters.Should().NotBeEmpty().And.HaveCount(2);
			AssertIsTermFilter(Filter1, orFilter.Filters.First().Term);
			AssertIsTermFilter(Filter2, orFilter.Filters.Last().Term);
		}
		
	}
}