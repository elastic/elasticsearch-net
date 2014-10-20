using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
	[TestFixture]
	public class CombinationTests : IntegrationTests
	{
		[Test]
		public void Percentiles()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Stats("stats_agg", t=>t.Field(p=>p.LOC))
					.Percentiles("bucket_agg", m => m
						.Field(p => p.IntValues)
						.Percentages(97,99,99.9)
					)
				)
			);
			var percentilesAgg = results.Aggs.Percentiles("bucket_agg");
			percentilesAgg.Should().NotBeNull();
			percentilesAgg.Items.Should().NotBeEmpty().And.HaveCount(3);

	        var statsBucket = results.Aggs.Stats("stats_agg");
	        statsBucket.Should().NotBeNull();
	        statsBucket.Sum.Should().BeGreaterThan(1);
		}

		[Test]
		public void Terms()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Terms("stats_agg", t=>t.Field(p=>p.Name))
					.Percentiles("bucket_agg", m => m
						.Field(p => p.IntValues)
						.Percentages(97,99,99.9)
					)
				)
			);
			var percentilesAgg = results.Aggs.Percentiles("bucket_agg");
			percentilesAgg.Should().NotBeNull();
			percentilesAgg.Items.Should().NotBeEmpty().And.HaveCount(3);

	        var statsBucket = results.Aggs.Terms("stats_agg");
	        statsBucket.Should().NotBeNull();
	        statsBucket.Items.Should().NotBeEmpty();
		}
		
		[Test]
		public void Stats()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Percentiles("bucket_agg", m => m
						.Field(p => p.IntValues)
						.Percentages(97,99,99.9)
					)
					.Stats("stats_agg", t=>t.Field(p=>p.LOC))
				)
			);
			var percentilesAgg = results.Aggs.Percentiles("bucket_agg");
			percentilesAgg.Should().NotBeNull();
			percentilesAgg.Items.Should().NotBeEmpty().And.HaveCount(3);

	        var statsBucket = results.Aggs.Stats("stats_agg");
	        statsBucket.Should().NotBeNull();
	        statsBucket.Sum.Should().BeGreaterThan(1);
		}

		[Test]
		public void DateHistogram()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("bucket_agg", m => m
						.Field(p => p.StartedOn)
						.Interval("1d")
					)
					.Stats("stats_agg", t=>t.Field(p=>p.LOC))
				)
			);
			var dateHisto = results.Aggs.DateHistogram("bucket_agg");
			dateHisto.Should().NotBeNull();
			dateHisto.Items.Should().NotBeEmpty();

	        var statsBucket = results.Aggs.Stats("stats_agg");
	        statsBucket.Should().NotBeNull();
	        statsBucket.Sum.Should().BeGreaterThan(1);
		}
		
		[Test]
		public void DateHistogramMultiple()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("date1", m => m
						.Field(p => p.StartedOn)
						.Interval("1d")
					)
					.DateHistogram("date2", m => m
						.Field(p => p.StartedOn)
						.Interval("1d")
					)
				)
			);
			var dateHisto = results.Aggs.DateHistogram("date1");
			dateHisto.Should().NotBeNull();
			dateHisto.Items.Should().NotBeEmpty();

			var statsBucket = results.Aggs.DateHistogram("date2");
	        statsBucket.Should().NotBeNull();
			dateHisto.Items.Should().NotBeEmpty();
		}
	}
}