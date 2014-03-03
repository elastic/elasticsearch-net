using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
    [TestFixture]
    public class NestedBucketAggregationTests : IntegrationTests
    {
	    [Test]
	    public void Terms()
	    {
		    var results = this._client.Search<ElasticsearchProject>(s=>s
				.Size(0)
				.Aggregations(a=>a
					.Nested("followers", n=>n
						.Aggregations(t=>t
							.Terms("bucket_agg", m=>m.Field(p=>p.Country))
						)
					)
				)
			);
		    results.IsValid.Should().BeTrue();
		    var bucket = results.Aggs.Terms("bucket_agg");
		    bucket.Items.Should().NotBeEmpty();
	    }
	
    }
}