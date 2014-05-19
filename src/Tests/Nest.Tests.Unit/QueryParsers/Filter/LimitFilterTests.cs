using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class LimitFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Limit_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var limitFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Limit,
				f=>f.Limit(100)
				);
			limitFilter.Value.Should().Be(100);
		}
		
	}
}