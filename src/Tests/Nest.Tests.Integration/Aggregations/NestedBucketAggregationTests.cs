using System.Linq;
using System.Security.AccessControl;
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
					.Nested("contributors", n=>n
						.Path(p=>p.Contributors)
						.Aggregations(t=>t
							.Average("avg_age", m=>m
								.Field(p=>p.Contributors.First().Age)
							)
						)
					)
				)
			);

			//using the helper to return typed aggregation buckets
		    results.IsValid.Should().BeTrue();
		    var bucket = results.Aggs.Nested("contributors");
		    bucket.DocCount.Should().BeGreaterThan(1);

		    var averageAge = bucket.Average("avg_age");
		    averageAge.Should().NotBeNull();
		    averageAge.Value.Should().HaveValue()
			    .And.BeGreaterOrEqualTo(18);

			//Using the .Aggregation dictionary.
			var contributors = results.Aggregations["contributors"] as SingleBucket;
			contributors.Should().NotBeNull();
			contributors.DocCount.Should().BeGreaterThan(1);
			averageAge = contributors.Aggregations["avg_age"] as ValueMetric;
		    averageAge.Should().NotBeNull();
		    averageAge.Value.Should().HaveValue()
			    .And.BeGreaterOrEqualTo(18);
	    }
	
    }
}