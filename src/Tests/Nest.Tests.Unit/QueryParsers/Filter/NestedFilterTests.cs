using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class NestedFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Nested_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var nestedFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Nested,
				f=>f.Nested(n=>n
					.Score(NestedScore.Max)
					.Path(p=>p.Followers[0])
					.Query(q=>q.Term(p=>p.Followers[0].FirstName,"elasticsearch.pm"))
					)
				);
			nestedFilter.Path.Should().Be("followers");
			nestedFilter.Score.Should().Be(NestedScore.Max);
			var query = nestedFilter.Query;
			query.Should().NotBeNull();
			var termQuery = query.Term;
			termQuery.Field.Should().Be("followers.firstName");
			termQuery.Value.Should().Be("elasticsearch.pm");
		}
		
		
	}
}