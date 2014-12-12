using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
	[TestFixture]
	public class SingleBucketAggregationTests : IntegrationTests
	{
		[Test]
		public void Missing()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Missing("miss_me", m => m.Field("not_really"))
				)
			);
			results.IsValid.Should().BeTrue();
			var missingBucket = results.Aggs.Missing("miss_me");
			missingBucket.DocCount.Should().BeGreaterThan(1);
		}

		[Test]
		public void Filter()
		{
			var lookFor = NestTestData.Data.Select(p => p.Country).First();
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Filter("filtered_agg", m => m
						.Filter(f => f.Term(p => p.Country, lookFor))
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var filteredBucket = results.Aggs.Filter("filtered_agg");
			filteredBucket.DocCount.Should().BeGreaterThan(0);
		}

		[Test]
		public void Global()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Query(q => q.Term(p => p.Name, "There is no name like this"))
				.Aggregations(a => a
					.Global("global", g => g
						.Aggregations(aa => aa
							.ValueCount("name", n => n.Field(p => p.Name))
						)
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var global = results.Aggs.Global("global");
			global.DocCount.Should().BeGreaterThan(1);
			var valueCount = global.ValueCount("name");
			valueCount.Value.Should().BeGreaterThan(1);
		}
		
		[Test]
		public void Children()
		{
			var results = this.Client.Search<Parent>(s => s
				.Size(0)
				.Aggregations(a => a
					.Terms("top-parents", ta => ta
						.MinimumDocumentCount(1)
						.Field(p => p.ParentName)
						.Size(10)
						.Aggregations(aa => aa
							.Children<Child>("to-children", ca => ca
								.Aggregations(aaa => aaa
									.Terms("top-children", taa => taa
										.Field("child.childName")
										.Size(10)
									)
								)
							)
						)
					)
				)
			);

			results.IsValid.Should().BeTrue();
			var topParents = results.Aggs.Terms("top-parents");
			topParents.Should().NotBeNull();
			topParents.Items.Should().NotBeEmpty();
			foreach (var topParent in topParents.Items)
			{
				topParent.DocCount.Should().BeGreaterThan(0);
				var children = topParent.Children("to-children");
				if (children.DocCount > 0)
				{
					var topChildren = children.Terms("top-children");
					topChildren.Items.Should().NotBeEmpty();
					foreach (var topChild in topChildren.Items)
						topChild.DocCount.Should().BeGreaterThan(0);
				}
			}
		}

		[Test]
		[SkipVersion("0 - 1.3.9", "Children aggregation added in ES 1.4")]
		public void Children_Typed()
		{
			var results = this.Client.Search<Parent>(s => s
				.Size(0)
				.Aggregations(a => a
					.Terms("top-parents", ta => ta
						.MinimumDocumentCount(1)
						.Field(p => p.ParentName)
						.Size(10)
						.Aggregations(aa => aa
							.Children<Child>("to-children", ca => ca
								.Aggregations(aaa => aaa
									.Terms("top-children", taa => taa
										.Field(f=>f.FullyQualified().ChildName)
										.Size(10)
									)
								)
							)
						)
					)
				)
			);

			results.IsValid.Should().BeTrue();
			var topParents = results.Aggs.Terms("top-parents");
			topParents.Should().NotBeNull();
			topParents.Items.Should().NotBeEmpty();
			foreach (var topParent in topParents.Items)
			{
				topParent.DocCount.Should().BeGreaterThan(0);
				var children = topParent.Children("to-children");
				if (children.DocCount > 0)
				{
					var topChildren = children.Terms("top-children");
					topChildren.Items.Should().NotBeEmpty();
					foreach (var topChild in topChildren.Items)
						topChild.DocCount.Should().BeGreaterThan(0);
				}
			}
		}
	}
}