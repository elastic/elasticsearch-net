using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Aggregations.Terms
{
	[TestFixture]
	public class TermsAggregationTests : BaseJsonTests
	{
		[Test]
		public void TermsAggregationSerializes()
		{
			var s = new TermsAggregationDescriptor<ElasticsearchProject>()
				.CollectMode(TermsAggregationCollectMode.BreadthFirst)
				.ExecutionHint(TermsAggregationExecutionHint.GlobalOrdinalsLowCardinality)
				.Field(p => p.Country)
				.MinimumDocumentCount(1)
				.OrderAscending("_count");

			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}

		[Test]
		public void TermsAggregation_IncludeExclude_Array_Serializes()
		{
			var s = new TermsAggregationDescriptor<ElasticsearchProject>()
				.Field(p => p.Name)
				.Include(new[] { "value1", "value2" })
				.Exclude(new[] { "value3" });

			var expected = @"{
				field: ""name"",
				include: [ ""value1"", ""value2"" ],
				exclude: [ ""value3"" ]
			}";

			var actual = TestElasticClient.Serialize(s);
			Assert.True(actual.JsonEquals(expected));
		}

		[Test]
		public void TermsAggregation_IncludeExclude_Pattern_Serializes()
		{
			var s = new TermsAggregationDescriptor<ElasticsearchProject>()
				.Field(p => p.Name)
				.Include("elastic*", "CANON_EQ|CASE_INSENSITIVE")
				.Exclude("nest*");

			var expected = @"{
				field: ""name"",
				include: {
					pattern: ""elastic*"",
					flags: ""CANON_EQ|CASE_INSENSITIVE""
				},
				exclude: {
					pattern: ""nest*""
				}
			}";

			var actual = TestElasticClient.Serialize(s);
			Assert.True(actual.JsonEquals(expected));
		}
	}
}
