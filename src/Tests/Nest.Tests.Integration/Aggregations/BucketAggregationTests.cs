using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
	[TestFixture]
	public class BucketAggregationTests : IntegrationTests
	{
		[Test]
		public void Terms()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Terms("bucket_agg", m => m.Field(p => p.Country))
				)
			);
			results.IsValid.Should().BeTrue();
			var bucket = results.Aggs.Terms("bucket_agg");
			bucket.Items.Should().NotBeEmpty();
		}
		
		[Test]
		[SkipVersion("0 - 1.0.9", "Significant terms aggregation not introduced until 1.1")]
		public void SignificantTerms()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Terms("term_items", gh=>gh
						.Field(p=>p.Content)
						.Aggregations(gha=>gha
							.SignificantTerms("bucket_agg", m => m
								.Field(p => p.Content)
								.Size(2)
								.Aggregations(ma=>ma.Terms("country", t=>t.Field(p=>p.Country)))
							)
						)
					)
				)
			);
			results.IsValid.Should().BeTrue();

			var bucket = results.Aggs.Terms("term_items");
			bucket.Items.Should().NotBeEmpty();

			foreach (var item in bucket.Items)
			{
				item.DocCount.Should().BeGreaterThan(0);
				var significantTerms = item.SignificantTerms("bucket_agg");
				significantTerms.Should().NotBeNull();
				significantTerms.DocCount.Should().BeGreaterThan(0);
				significantTerms.Items.Should().NotBeNull().And.NotBeEmpty();
				foreach (var sigTerm in significantTerms.Items)
				{
					sigTerm.BgCount.Should().BeGreaterThan(0);
					sigTerm.Score.Should().BeGreaterThan(0);
					var sigTermTerms = sigTerm.Terms("country");
					sigTermTerms.Should().NotBeNull();
					sigTermTerms.Items.Should().NotBeEmpty();
				}
			}

		}
		[Test]
		public void Histogram()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Histogram("bucket_agg", m => m
						.Field(p => p.IntValues)
						.Interval(10)
					)

				)
			);
			results.IsValid.Should().BeTrue();
			var bucket = results.Aggs.Histogram("bucket_agg");
			bucket.Items.Should().NotBeEmpty();
		}

		[Test]
		[SkipVersion("0 - 1.0.9", "Percentiles aggregation not introduced until 1.1")]
		public void Percentiles()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Percentiles("bucket_agg", m => m
						.Field(p => p.IntValues)
					)

				)
			);
			results.IsValid.Should().BeTrue();
			var metric = results.Aggs.Percentiles("bucket_agg");
			metric.Should().NotBeNull();
			metric.Items.Should().NotBeEmpty();

			var lastPercentile = metric.Items.Last();
			lastPercentile.Percentile.Should().Be(99.0);
			lastPercentile.Value.Should().BeGreaterThan(1);
			
		}
		
		[Test]
		[SkipVersion("0 - 1.0.9", "Percentiles aggregation not introduced until 1.1")]
		public void PercentilesCustom()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Percentiles("bucket_agg", m => m
						.Field(p => p.IntValues)
						.Percentages(97,99,99.9)
					)

				)
			);
			results.IsValid.Should().BeTrue();
			var metric = results.Aggs.Percentiles("bucket_agg");
			metric.Should().NotBeNull();
			metric.Items.Should().NotBeEmpty().And.HaveCount(3);

			var lastPercentile = metric.Items.Last();
			lastPercentile.Percentile.Should().Be(99.9);
			lastPercentile.Value.Should().BeGreaterThan(1.5);
			
		}
		
		[Test]
		public void GeoHash()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.GeoHash("bucket_agg", m => m
						.Field(p => p.Origin)
						.GeoHashPrecision(GeoHashPrecision.Precision2)
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var bucket = results.Aggs.GeoHash("bucket_agg");
			bucket.Items.Should().NotBeEmpty()
				.And.OnlyContain(i => !i.Key.IsNullOrEmpty());
		}
		[Test]
		public void Range()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Range("bucket_agg", m => m
						.Field(p => p.LongValue)
						.Ranges(
							r => r.To(10),
							r => r.From(10)
						)
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var bucket = results.Aggs.Range("bucket_agg");
			bucket.Items.Should().NotBeEmpty();
		}
		[Test]
		public void DateRange()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.DateRange("bucket_agg", m => m
						.Field(p => p.StartedOn)
						.Ranges(
							r => r.To("now-10M/M"),
							r => r.From("now-10M/M")
						)
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var bucket = results.Aggs.DateRange("bucket_agg");
			bucket.Items.Should().NotBeEmpty();
		}
		[Test]
		public void IpRange()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.IpRange("bucket_agg", dh => dh
						.Field(p => p.PingIP)
						.Ranges(r=>r.Mask("10.0.0.0/25"))
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var bucket = results.Aggs.IpRange("bucket_agg");
			bucket.Items.Should().NotBeEmpty();
		}
		[Test]
		public void GeoDistance()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.GeoDistance("bucket_agg", dh => dh
						.Field(p => p.Origin)
						.Origin(28.0, 28.0)
						.Unit(GeoUnit.Kilometers)
						.Ranges(
							r => r.To(1),
							r => r.From(1).To(100)
						)
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var bucket = results.Aggs.GeoDistance("bucket_agg");
			bucket.Items.Should().NotBeEmpty();
		}
		[Test]
		public void DateHistogram()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.DateHistogram("bucket_agg", dh => dh
						.Field(p => p.StartedOn)
						.Interval("1d")
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var bucket = results.Aggs.DateHistogram("bucket_agg");
			bucket.Items.Should().NotBeEmpty();
			var item = bucket.Items.First();
		}

	}
}