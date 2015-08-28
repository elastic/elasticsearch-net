using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class HasParentFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void HasParent_Query_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var hasParentFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.HasParent,
				f=>f.HasParent<ElasticsearchProject>(d=>d
					.InnerHits()
					.Query(q=>q.Term(p=>p.Country, "value"))
				)
			);
			hasParentFilter.InnerHits.Should().NotBeNull();

			var query = hasParentFilter.Query;
			query.Should().NotBeNull();
			query.Term.Field.Should().Be("country");
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void HasParent_Filter_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var hasParentFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.HasParent,
				f=>f.HasParent<ElasticsearchProject>(d=>d
					.InnerHits()
					.Filter(pf=>pf.Term(p=>p.Country,"value"))
				)
			);
			hasParentFilter.InnerHits.Should().NotBeNull();

			var filter = hasParentFilter.Filter;
			filter.Should().NotBeNull();
			filter.Term.Field.Should().Be("country");
		}
	}
}