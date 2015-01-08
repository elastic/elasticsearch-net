using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Collections.Generic;

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
		[SkipVersion("0 - 1.2.9", "Percentile ranks agg added in 1.3")]
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
		[SkipVersion("0 - 1.3.0", "Fails against 1.3: https://github.com/elasticsearch/elasticsearch/issues/7004")]
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

		[Test]
		[SkipVersion("0 - 1.2.9", "Top hits agg added in 1.3")]
		public void TopHits()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Query(q => q
					.Match(m => m
						.OnField(p => p.Name)
						.Query("elasticsearch")
					)
				)
				.Aggregations(a => a
					.Terms("top-countries", t => t
						.Field(p => p.Country)
						.Size(3)
						.Aggregations(aa => aa
							.TopHits("top-country-hits", th => th
								.Sort(sort => sort
									.OnField(p => p.StartedOn)
									.Order(SortOrder.Descending)
								)
								.Source(src => src
									.Include(p => p.Name)
								)
								.Size(1)
								.Explain(true)
								.Version(true)
								.Highlight(h => h
									.PreTags("<em>")
									.PostTags("</em>")
									.OnFields(hf => hf
										.OnField(p => p.Name)
										.PreTags("<em>")
										.PostTags("</em>")
									)
								)
								.ScriptFields(sf => sf
									.Add("locscriptfield", sff => sff
										.Script("doc['loc'].value * multiplier")
										.Params(sp => sp
											.Add("multiplier", 2)
										)
									)
								)
								.FieldDataFields(p => p.Name, p => p.Country)
							)
						)
					)
				)
			);

			results.IsValid.Should().BeTrue();

			var topCountries = results.Aggs.Terms("top-countries").Items;
			foreach(var topCountry in topCountries)
			{
				var topHits = topCountry.TopHits("top-country-hits");
				topHits.Should().NotBeNull();
				topHits.Total.Should().BeGreaterThan(0);
				var hits = topHits.Hits<ElasticsearchProject>();
				hits.Should().NotBeEmpty().And.NotContain(h=> h.Id.IsNullOrEmpty() || h.Index.IsNullOrEmpty());
				hits.All(h => h.Explanation != null).Should().BeTrue();
				hits.All(h => !h.Version.IsNullOrEmpty()).Should().BeTrue();
				hits.All(h => h.Highlights.Count() > 0).Should().BeTrue();
				hits.All(h => h.Fields.FieldValues<int[]>("locscriptfield").HasAny()).Should().BeTrue();
				hits.All(h => h.Fields.FieldValues<string[]>("name").HasAny()).Should().BeTrue();
				hits.All(h => h.Fields.FieldValues<string[]>("country").HasAny()).Should().BeTrue();
				topHits.Documents<ElasticsearchProject>().Should().NotBeEmpty();
			}
		}

		[Test]
		[SkipVersion("0 - 1.3.9", "Scripted metric aggregation added in ES 1.4")]
		public void ScriptedMetric_SingleNumericValue()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.ScriptedMetric("project_count", sm => sm
						.InitScript("_agg['count'] = []")
						.MapScript("_agg.count.add(1)")
						.CombineScript("total = 0; for (c in _agg.count) { total += c }; return total")
						.ReduceScript("total = 0; for (a in _aggs) { total += a }; return total")
					)
				)
			);

			results.IsValid.Should().BeTrue();
			var count = results.Aggs.ScriptedMetric("project_count");
			var value = count.Value<double>();
			value.Should().BeGreaterThan(0);
		}

		[SkipVersion("0 - 1.3.9", "Scripted metric aggregation added in ES 1.4")]
		[Test]
		public void ScriptedMetric_MultiNumericValue()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.ScriptedMetric("project_count_per_shard", sm => sm
						.InitScript("_agg['count'] = []")
						.MapScript("_agg.count.add(1)")
						.CombineScript("total = 0; for (c in _agg.count) { total += c }; return total")
						.ReduceScript("return _aggs")
					)
				)
			);

			results.IsValid.Should().BeTrue();
			var countsPerShard = results.Aggs.ScriptedMetric("project_count_per_shard");
			var value = countsPerShard.Value<IEnumerable<double>>();
			value.Should().NotBeNull();
			value.Count().Should().BeGreaterThan(0);
		}

		[Test]
		[SkipVersion("0 - 1.3.9", "Scripted metric aggregation added in ES 1.4")]
		public void ScriptedMetric_SingleStringValue()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.ScriptedMetric("first_name", sm => sm
						.InitScript("_agg['names'] = []")
						.MapScript("_agg.names.add(doc['name'].value)")
						.ReduceScript("return _aggs['names'][0][0]")
					)
				)
			);

			results.IsValid.Should().BeTrue();
			var firstName = results.Aggs.ScriptedMetric("first_name");
			var value = firstName.Value<string>();
			value.Should().NotBeNullOrEmpty();
		}

		[Test]
		[SkipVersion("0 - 1.3.9", "Scripted metric aggregation added in ES 1.4")]
		public void ScriptedMetric_MultiStringValue()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.ScriptedMetric("names_on_first_shard", sm => sm
						.InitScript("_agg['names'] = []")
						.MapScript("_agg.names.add(doc['name'].value)")
						.ReduceScript("return _aggs['names'][0]")
					)
				)
			);

			results.IsValid.Should().BeTrue();
			var namesOnFirstShard = results.Aggs.ScriptedMetric("names_on_first_shard");
			var value = namesOnFirstShard.Value<IEnumerable<string>>();
			value.Should().NotBeNull();
			value.Count().Should().BeGreaterThan(0);
		}

		[Test]
		[SkipVersion("0 - 1.3.9", "Scripted metric aggregation added in ES 1.4")]
		public void ScriptedMetric_MultiArrayValue()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.ScriptedMetric("names_per_shard", sm => sm
						.InitScript("_agg['names'] = []")
						.MapScript("_agg.names.add(doc['name'].value)")
						.ReduceScript("return _aggs['names']")
					)
				)
			);

			results.IsValid.Should().BeTrue();
			var namesPerShard = results.Aggs.ScriptedMetric("names_per_shard");
			var value = namesPerShard.Value<IEnumerable<IEnumerable<string>>>();
			value.Should().NotBeNull();
		}
	}
}