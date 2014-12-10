using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
    [TestFixture]
    public class FiltersAggregationTests : IntegrationTests
    {
        [Test]
        public void NamedFilters()
        {
            var results = this.Client.Search<ElasticsearchProject>(s => s
                .Size(0)
                .Aggregations(a => a
                    .Filters("filtersAggregation",
                        t =>
                            t.Filters(x => x.Name("first").Term(p => p.Name, "pyelasticsearch"),
                                x => x.Name("second").Term(p => p.Country, "Sweden"))
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
            var firstCount = firstBucket.DocCount;
            firstCount.ShouldBeEquivalentTo(1);

            var secondBucket = filtersBuckets.Aggregations.Global("second");
            secondBucket.Should().NotBeNull();
            var secondCount = secondBucket.DocCount;
            secondCount.ShouldBeEquivalentTo(2);
        }

        [Test]
        public void AnonymousFilters()
        {
            var results = this.Client.Search<ElasticsearchProject>(s => s
                .Size(0)
                .Aggregations(a => a
                    .Filters("filtersAggregation",
                        t =>
                            t.Filters(x => x.Term(p => p.Name, "pyelasticsearch"), x => x.Term(p => p.Country, "Sweden"))
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
                .And.Contain(a => (a as SingleBucket).DocCount == 1)
                .And.Contain(a => (a as SingleBucket).DocCount == 2);

        }
    }
}