using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
	[TestFixture]
	public class MetricAggregationTests : IntegrationTests
	{
		[Test]
		public void WrongFieldName()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Min("value_agg", t => t
						.Field("this_field_name_does_not_exist")
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var termBucket = results.Aggs.Min("value_agg");
			termBucket.Should().NotBeNull();
			termBucket.Value.Should().NotHaveValue();
		}

		[Test]
		public void Average()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Average("value_agg", t => t.Field(p => p.LOC))
				)
			);
			results.IsValid.Should().BeTrue();
			var termBucket = results.Aggs.Min("value_agg");
			termBucket.Should().NotBeNull();
			termBucket.Value.Should().HaveValue();
		}
		[Test]
		public void Min()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Min("value_agg", t => t.Field(p => p.LOC))
				)
			);
			results.IsValid.Should().BeTrue();
			var termBucket = results.Aggs.Min("value_agg");
			termBucket.Should().NotBeNull();
			termBucket.Value.Should().HaveValue();
		}
		
		[Test]
		[SkipVersion("0 - 1.0.9", "Cardinality aggregation not introduced until 1.1")]
		public void Cardinality()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Cardinality("bucket_agg", m => m
						.Field(p => p.Country)
					)

				)
			);
			results.IsValid.Should().BeTrue();
			var metric = results.Aggs.Cardinality("bucket_agg");
			metric.Should().NotBeNull();
			metric.Value.Should().HaveValue();
			metric.Value.Value.Should().BeGreaterThan(0);

		}

		[Test]
		public void Max()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Max("value_agg", t => t.Field(p => p.LOC))
				)
			);
			results.IsValid.Should().BeTrue();
			var termBucket = results.Aggs.Max("value_agg");
			termBucket.Should().NotBeNull();
			termBucket.Value.Should().HaveValue();
		}

		[Test]
		public void Sum()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Sum("value_agg", t => t.Field(p => p.LOC))
				)
			);
			results.IsValid.Should().BeTrue();
			var termBucket = results.Aggs.Sum("value_agg");
			termBucket.Should().NotBeNull();
			termBucket.Value.Should().HaveValue();
		}

		[Test]
		public void ValueCount()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.ValueCount("value_agg", t => t.Field(p => p.LOC))
				)
			);
			results.IsValid.Should().BeTrue();
			var termBucket = results.Aggs.ValueCount("value_agg");
			termBucket.Should().NotBeNull();
			termBucket.Value.Should().HaveValue();
		}

		[Test]
		public void PercentilesRank()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.PercentileRanks("percentile_ranks_agg", pr => pr
						.Field(p => p.LongValue)
						.Values(new double [] { 15, 30 })
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var percentiles = results.Aggs.PercentilesRank("percentile_ranks_agg");
			percentiles.Should().NotBeNull();
			percentiles.Items.Count.Should().BeGreaterThan(0);
		}

		[Test]
		public void GeoBounds()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.GeoBounds("viewport", g => g
						.Field(p => p.Origin)
						.WrapLongitude()
					)
				)
			);

			results.IsValid.Should().BeTrue();
			var geoBoundsMetric = results.Aggs.GeoBounds("viewport");
			geoBoundsMetric.Should().NotBeNull();
			geoBoundsMetric.Bounds.Should().NotBeNull();
			geoBoundsMetric.Bounds.TopLeft.Should().NotBeNull();
			geoBoundsMetric.Bounds.TopLeft.Lat.Should().NotBe(0);
			geoBoundsMetric.Bounds.TopLeft.Lon.Should().NotBe(0);
			geoBoundsMetric.Bounds.BottomRight.Should().NotBeNull();
			geoBoundsMetric.Bounds.BottomRight.Lat.Should().NotBe(0);
			geoBoundsMetric.Bounds.BottomRight.Lon.Should().NotBe(0);
		}
	}
}