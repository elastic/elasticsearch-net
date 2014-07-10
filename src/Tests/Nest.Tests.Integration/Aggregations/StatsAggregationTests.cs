using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
    [TestFixture]
    public class StatsAggregation : IntegrationTests
    {
        [Test]
        public void WrongFieldName()
        {
			var results = this._client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.Stats("stats_agg", t=>t
						.Field("this_field_name_does_not_exist")
					)
				)
			);
	        results.IsValid.Should().BeTrue();
	        var statsBucket = results.Aggs.Stats("stats_agg");
	        statsBucket.Should().NotBeNull();
	        statsBucket.Count.Should().Be(0);
	        statsBucket.Sum.Should().NotHaveValue();
        }
		
		[Test]
        public void Average()
        {
			var results = this._client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.Stats("stats_agg", t=>t.Field(p=>p.LOC))
				)
			);
	        results.IsValid.Should().BeTrue();
	        var statsBucket = results.Aggs.Stats("stats_agg");
	        statsBucket.Should().NotBeNull();
	        statsBucket.Count.Should().BeGreaterThan(1);
	        statsBucket.Sum.Should().BeGreaterThan(1);
        }

		[Test]
        public void ExtendedStats()
        {
			
			var results = this._client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.ExtendedStats("stats_agg", t=>t.Field(p=>p.LOC))
				)
			);
	        results.IsValid.Should().BeTrue();
	        var statsBucket = results.Aggs.ExtendedStats("stats_agg");
	        statsBucket.Should().NotBeNull();
	        statsBucket.Count.Should().BeGreaterThan(1);
	        statsBucket.StdDeviation.Should().BeGreaterThan(1);
        }	
		
    }
}