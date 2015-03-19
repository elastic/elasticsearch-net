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
			var results = this.Client.Search<ElasticsearchProject>(s => s
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
			var results = this.Client.Search<ElasticsearchProject>(s => s
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
		[SkipVersion("0 - 1.0.9", "Percentiles agg added in 1.1")]
        public void StatsAllowsOtherAggsOnTheSameLevel()
        {
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.Stats("stats_agg", t=>t.Field(p=>p.LOC))
					.Percentiles("bucket_agg", m => m
						.Field(p => p.IntValues)
						.Percentages(97,99,99.9)
					)
				)
			);
	        results.IsValid.Should().BeTrue();
			var percentilesAgg = results.Aggs.Percentiles("bucket_agg");
			percentilesAgg.Should().NotBeNull();
			percentilesAgg.Items.Should().NotBeEmpty().And.HaveCount(3);

	        var statsBucket = results.Aggs.Stats("stats_agg");
	        statsBucket.Should().NotBeNull();
	        statsBucket.Count.Should().BeGreaterThan(1);
	        statsBucket.Sum.Should().BeGreaterThan(1);
        }
		[Test]
        public void ExtendedStats()
        {
			
			var results = this.Client.Search<ElasticsearchProject>(s => s
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

		[Test]
		[SkipVersion("0 - 1.4.2", "Standard deviation bounds added in 1.4.3")]
		public void ExtendedStatsWithStandardDeviationBounds()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.ExtendedStats("stats_agg", t => t.Field(p => p.LOC))
				)
			);
			results.IsValid.Should().BeTrue();
			var statsBucket = results.Aggs.ExtendedStats("stats_agg");
			statsBucket.Should().NotBeNull();
			statsBucket.Count.Should().BeGreaterThan(1);
			statsBucket.StdDeviation.Should().BeGreaterThan(1);
			statsBucket.StdDeviationBounds.Should().NotBeNull();
			statsBucket.StdDeviationBounds.Upper.Should().NotBe(0);
			statsBucket.StdDeviationBounds.Lower.Should().NotBe(0);
		}
		
    }
}