using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
	[TestFixture]
	public class NestedBucketAggregationTests : IntegrationTests
	{
		[Test]
		public void Terms()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Nested("contributors", n => n
						.Path(p => p.Contributors)
						.Aggregations(t => t
							.Average("avg_age", m => m
								.Field(p => p.Contributors.First().Age)
							)
						)
					)
				)
			);

			//using the helper to return typed aggregation buckets
			results.IsValid.Should().BeTrue();
			var bucket = results.Aggs.Nested("contributors");
			bucket.DocCount.Should().BeGreaterThan(1);

			var averageAge = bucket.Average("avg_age");
			averageAge.Should().NotBeNull();
			averageAge.Value.Should().HaveValue()
				.And.BeGreaterOrEqualTo(18);

			//Using the .Aggregation dictionary.
			var contributors = results.Aggregations["contributors"] as SingleBucket;
			contributors.Should().NotBeNull();
			contributors.DocCount.Should().BeGreaterThan(1);
			averageAge = contributors.Aggregations["avg_age"] as ValueMetric;
			averageAge.Should().NotBeNull();
			averageAge.Value.Should().HaveValue()
				.And.BeGreaterOrEqualTo(18);
		}

		[Test]
		public void MultipleTermsInTerms()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Nested("contributors", n => n
						.Path(p => p.Contributors)
						.Aggregations(na => na
							.Terms("ids", t=>t.Field(p=>p.Contributors.First().Id))
							.Terms("name", t=>t.Field(p=>p.Contributors.First().LastName))
						)
					)
				)
			);

			//using the helper to return typed aggregation buckets
			results.IsValid.Should().BeTrue();
			var bucket = results.Aggs.Nested("contributors");
			bucket.DocCount.Should().BeGreaterThan(1);

			var ids = bucket.Terms("ids");
			ids.Should().NotBeNull();
			ids.Items.Should().NotBeEmpty();

			var countryTerms = bucket.Terms("name");
			countryTerms.Should().NotBeNull();
			countryTerms.Items.Should().NotBeEmpty();

		}

		[Test]
		[SkipVersion("0 - 1.1.9", "Reverse nested agg added in 1.2.0")]
		public void ReverseNested()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Nested("contributors", n => n
						.Path(p => p.Contributors)
						.Aggregations(t => t
							.Terms("ages", m => m
								.Field(p => p.Contributors.First().Age)
								.Aggregations(aa => aa
									.ReverseNested("contributor_to_project", rn => rn
										.Aggregations(aaa => aaa
											.Terms("countries_per_age", tt => tt
												.Field(p => p.Country)
											)
										)
									)
								)
							)
						)
					)
				)
			);

			results.IsValid.Should().BeTrue();
			var contributors = results.Aggs.Nested("contributors");
			var ages = contributors.Aggregations["ages"] as Bucket;

			foreach (var item in ages.Items)
			{
				var age = item as KeyItem;
				age.Key.Should().NotBeNullOrWhiteSpace();
				var contributorToProject = age.Aggregations["contributor_to_project"] as SingleBucket;
				var countriesPerAge = contributorToProject.Terms("countries_per_age");
				countriesPerAge.Items.Count().Should().BeGreaterThan(0);
			}
		}
	}
}