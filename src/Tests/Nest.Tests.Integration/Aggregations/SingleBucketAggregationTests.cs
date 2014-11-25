using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
    [TestFixture]
    public class SingleBucketAggregationTests : IntegrationTests
    {
	    [Test]
	    public void Missing()
	    {
		    var results = this.Client.Search<ElasticsearchProject>(s=>s
				.Size(0)
				.Aggregations(a=>a
					.Missing("miss_me", m=>m.Field("not_really"))
				)
			);
		    results.IsValid.Should().BeTrue();
		    var missingBucket = results.Aggs.Missing("miss_me");
		    missingBucket.DocCount.Should().BeGreaterThan(1);
	    }
		
		[Test]
	    public void Filter()
		{
			var lookFor = NestTestData.Data.Select(p => p.Country).First();
		    var results = this.Client.Search<ElasticsearchProject>(s=>s
				.Size(0)
				.Aggregations(a=>a
					.Filter("filtered_agg", m=>m
						.Filter(f=>f.Term(p=>p.Country, lookFor))
					)
				)
			);
		    results.IsValid.Should().BeTrue();
		    var filteredBucket = results.Aggs.Filter("filtered_agg");
		    filteredBucket.DocCount.Should().BeGreaterThan(0);
	    }
		
		[Test]
	    public void Global()
	    {
		    var results = this.Client.Search<ElasticsearchProject>(s=>s
				.Size(0)
				.Query(q=>q.Term(p=>p.Name, "There is no name like this"))
				.Aggregations(a=>a
					.Global("global", g=>g
						.Aggregations(aa=>aa
							.ValueCount("name",n=>n.Field(p=>p.Name))
						)
					)
				)
			);
		    results.IsValid.Should().BeTrue();
		    var global = results.Aggs.Global("global");
		    global.DocCount.Should().BeGreaterThan(1);
			var valueCount = global.ValueCount("name");
		    valueCount.Value.Should().BeGreaterThan(1);
	    }
    }
}