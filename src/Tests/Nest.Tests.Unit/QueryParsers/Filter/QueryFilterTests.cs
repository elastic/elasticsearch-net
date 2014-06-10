using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class QueryFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Query_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var queryFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Query,
				f=>f.Query(q=>q.Term(p=>p.Name,"elasticsearch.pm"))
				);
			queryFilter.Query.Should().NotBeNull();
		}
		
	}
}