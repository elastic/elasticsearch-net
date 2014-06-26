using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
    /// <summary>
    ///  Tests that test whether the query response can be successfully mapped or not
    /// </summary>
    [TestFixture]
    public class ParseResponseItemsTests : IntegrationTests
    {
        [Test]
        public void KeyItem()
        {
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.Terms("my_terms_agg", t=>t
						.Field(p=>p.Country)
						.Aggregations(ta=>ta
							.Average("averge", avg=>avg
								.Field(p=>p.LOC)
							)
						)
					)
					
				)
			);
	        var request = results.ConnectionStatus.Request.Utf8String();
	        results.IsValid.Should().BeTrue("{0}", request);
	        results.Aggregations.Should().HaveCount(1);
	        var firstAgg = results.Aggregations.First().Value as Bucket;
	        firstAgg.Items.Should().HaveCount(10);

			//request.Should().Contain("averge");
        }

	    [Test]
	    public void ValueMetric()
	    {
		    var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.Average("my_avg", avg=>avg
						.Field(p=>p.LOC)
					)
				)
			);
			 var request = results.ConnectionStatus.Request.Utf8String();
	        results.IsValid.Should().BeTrue("{0}", request);
	        results.Aggregations.Should().HaveCount(1);
	        var firstAgg = results.Aggregations.First().Value as ValueMetric;
		    firstAgg.Should().NotBeNull();
		    firstAgg.Value.Should().BeGreaterThan(1);
	    }
		
		[Test]
	    public void SingleBucket()
	    {
		    var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.Filter("my_filtered_agg", ff=>ff
						.Filter(f=>!f.Term(p=>p.Name, "nest"))
						.Aggregations(aa=>aa
							.Terms("my_terms", ta=>ta
								.Field(p=>p.Name)
							)
						)
					)
				)
			);
			var request = results.ConnectionStatus.Request.Utf8String();
	        results.IsValid.Should().BeTrue("{0}", request);
	        results.Aggregations.Should().HaveCount(1);
	        var firstAgg = results.Aggregations.First().Value as SingleBucket;
		    firstAgg.Should().NotBeNull();
		    firstAgg.DocCount.Should().BeGreaterThan(1);
		    firstAgg.Aggregations.Should().NotBeEmpty();
	    }
		
		[Test]
	    public void DateHistogramItem()
	    {
		    var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.DateHistogram("my_dateh", dh=>dh
						.Field(p=>p.StartedOn)
						.Interval("1d")
					)
				)
			);
			var request = results.ConnectionStatus.Request.Utf8String();
	        results.IsValid.Should().BeTrue("{0}", request);
	        results.Aggregations.Should().HaveCount(1);
	        var firstAgg = results.Aggregations.First().Value as Bucket;
		    firstAgg.Should().NotBeNull();
		    firstAgg.Items.Should().NotBeEmpty();
			var grams = firstAgg.Items.OfType<HistogramItem>();
			grams.Should().NotBeEmpty();
	    }
		
		[Test]
	    public void GeoDistanceItem()
	    {
		    var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.GeoDistance("my_geod", dh=>dh
						.Field(p=>p.Origin)
						.Origin(28.0, 28.0)
						.Unit(GeoUnit.km)
						.Ranges(
							r=>r.To(1),
							r=>r.From(1).To(100)
						)
					)
				)
			);
			var request = results.ConnectionStatus.Request.Utf8String();
	        results.IsValid.Should().BeTrue("{0}", request);
	        results.Aggregations.Should().HaveCount(1);
	        var firstAgg = results.Aggregations.First().Value as Bucket;
		    firstAgg.Should().NotBeNull();
		    firstAgg.Items.Should().NotBeEmpty();
			var grams = firstAgg.Items.OfType<RangeItem>();
			grams.Should().NotBeEmpty();
	    }
		
		[Test]
	    public void RangeItem()
	    {
		    var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.Range("my_geod", dh=>dh
						.Field(p=>p.LongValue)
						//.DistanceType(GeoDistanceType.sloppy_arc)
						.Ranges(
							r=>r.Key("small_longs").To(1),
							r=>r.From(1).To(100)
						)
					)
				)
			);
			var request = results.ConnectionStatus.Request.Utf8String();
	        results.IsValid.Should().BeTrue("{0}", request);
	        results.Aggregations.Should().HaveCount(1);
	        var firstAgg = results.Aggregations.First().Value as Bucket;
		    firstAgg.Should().NotBeNull();
		    firstAgg.Items.Should().NotBeEmpty();
			var grams = firstAgg.Items.OfType<RangeItem>();
			grams.Should().NotBeEmpty();
	    }
	
		[Test]
	    public void DateRangeItem()
	    {
		    var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.DateRange("my_geod", dh=>dh
						.Field(p=>p.StartedOn)
						.Ranges(
							r=>r.To("now-10M/M"),
							r=>r.From("now-10M/M")
						)
					)
				)
			);
			var request = results.ConnectionStatus.Request.Utf8String();
	        results.IsValid.Should().BeTrue("{0}", request);
	        results.Aggregations.Should().HaveCount(1);
	        var firstAgg = results.Aggregations.First().Value as Bucket;
		    firstAgg.Should().NotBeNull();
		    firstAgg.Items.Should().NotBeEmpty();
			var grams = firstAgg.Items.OfType<RangeItem>();
			grams.Should().NotBeEmpty();
	    }
		[Test]
	    public void IpRangeItem()
	    {
		    var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.IpRange("my_ip", dh=>dh
						.Field(p=>p.PingIP)
						.Ranges("10.0.0.0/25")
					)
				)
			);
			var request = results.ConnectionStatus.Request.Utf8String();
	        results.IsValid.Should().BeTrue("{0}", request);
	        results.Aggregations.Should().HaveCount(1);
	        var firstAgg = results.Aggregations.First().Value as Bucket;
		    firstAgg.Should().NotBeNull();
		    firstAgg.Items.Should().NotBeEmpty();
			var grams = firstAgg.Items.OfType<RangeItem>();
			grams.Should().NotBeEmpty();
	    }
		[Test]
	    public void StatsMetric()
	    {
		    var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.Stats("my_stats", avg=>avg
						.Field(p=>p.LongValue)
					)
				)
			);
			 var request = results.ConnectionStatus.Request.Utf8String();
	        results.IsValid.Should().BeTrue("{0}", request);
	        results.Aggregations.Should().HaveCount(1);
	        var firstAgg = results.Aggregations.First().Value as StatsMetric;
		    firstAgg.Should().NotBeNull();
		    firstAgg.Count.Should().BeGreaterThan(1);
		    firstAgg.Min.Should().NotBe(0);
		    firstAgg.Max.Should().NotBe(0);
		    firstAgg.Average.Should().NotBe(0);
		    firstAgg.Sum.Should().BeGreaterThan(1);
	    }
		
		[Test]
	    public void ExtendedStatsMetric()
	    {
		    var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a=>a
					.ExtendedStats("my_stats", avg=>avg
						.Field(p=>p.LongValue)
					)
				)
			);
			 var request = results.ConnectionStatus.Request.Utf8String();
	        results.IsValid.Should().BeTrue("{0}", request);
	        results.Aggregations.Should().HaveCount(1);
	        var firstAgg = results.Aggregations.First().Value as ExtendedStatsMetric;
		    firstAgg.Should().NotBeNull();
		    firstAgg.Count.Should().BeGreaterThan(1);
		    firstAgg.Min.Should().NotBe(0);
		    firstAgg.Max.Should().NotBe(0);
		    firstAgg.Average.Should().NotBe(0);
		    firstAgg.Sum.Should().BeGreaterThan(1);
		    firstAgg.SumOfSquares.Should().NotBe(0);
		    firstAgg.Variance.Should().NotBe(0);
		    firstAgg.StdDeviation.Should().NotBe(0);
	    }
    }
}