using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
	[TestFixture]
	public class FiltersAggregationTests : IntegrationTests
	{
		private readonly string _indexedName = NestTestData.Data[3].Name;
		private readonly string _indexedCountry = NestTestData.Data[2].Country;
		[Test]
		[SkipVersion("0 - 1.3.9", "Filters aggregation added in ES 1.4")]
		public void NamedFilters()
		{

			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Filters("filtersAggregation", t => t
						.Filters(
							x => x.Name("first").Term(p => p.Name.Suffix("sort"), _indexedName),
							x => x.Name("second").Term(p => p.Country, _indexedCountry)
						)
						.Aggregations(n => n.Average("avg", d => d.Field(x => x.Id)))
					)
				)
			);

			results.IsValid.Should().BeTrue();
			var filtersBuckets = results.Aggs.Filters("filtersAggregation");
			filtersBuckets.Should().NotBeNull();
			filtersBuckets.Aggregations.Should().NotBeNull();

			var firstBucket = filtersBuckets.Aggregations.Global("first");
			firstBucket.Should().NotBeNull();
			firstBucket.DocCount.Should().BeGreaterOrEqualTo(1);

			var secondBucket = filtersBuckets.Aggregations.Global("second");
			secondBucket.Should().NotBeNull();
			var secondCount = secondBucket.DocCount;
			secondCount.Should().BeGreaterOrEqualTo(1);
		}

		[Test]
		[SkipVersion("0 - 1.3.9", "Filters aggregation added in ES 1.4")]
		public void AnonymousFilters()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Filters("filtersAggregation", t => t
						.Filters(
							x => x.Term(p => p.Name.Suffix("sort"), _indexedName), 
							x => x.Term(p => p.Country, _indexedCountry)
						)
						.Aggregations(n => n.Average("avg", d => d.Field(x => x.Id)))
					)
				)
			);

			results.IsValid.Should().BeTrue();
			var filtersBuckets = results.Aggs.Filters("filtersAggregation");
			filtersBuckets.Should().NotBeNull();
			filtersBuckets.Items.Should().NotBeNull();

			var buckets = filtersBuckets.Items;
			buckets.Should().NotBeNull()
				.And.HaveCount(2)
				.And.OnlyContain(a => (a as SingleBucket).DocCount >= 1);

		}
	}
}