using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class WildCardQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void WildCard_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f => f.Wildcard,
				f => f.Wildcard(wq => wq
					.Boost(1.1)
					.Rewrite(RewriteMultiTerm.ConstantScoreBoolean)
					.OnField(p => p.Name)
					.Value("wild*")
					)
				);
			q.Boost.Should().Be(1.1);
			q.Field.Should().Be("name");
			q.Value.Should().Be("wild*");
			q.Rewrite.Should().Be(RewriteMultiTerm.ConstantScoreBoolean);
		}

		[Test]
		public void InitializerTests()
		{
			QueryContainer wildcardQuery = new WildcardQuery
			{
				Field = "my_prefix_field",
				Value = "value",
				Rewrite = RewriteMultiTerm.ConstantScoreBoolean
			};
			var searchRequest = new SearchRequest()
			{
				Query = wildcardQuery
			};

			var search = this._client.Search<ElasticsearchProject>(searchRequest);

			var request = search.RequestInformation.Request.Utf8String();
			request.Should().Contain("my_prefix_field");
			Assert.Pass(request);
		}

		[Test]
		public void InitializerTypedTests()
		{
			QueryContainer boolQuery =
				new WildcardQuery<ElasticsearchProject>(p => p.Name)
				{
					Value = "value",
					Rewrite = RewriteMultiTerm.ConstantScoreBoolean
				}
				&& new WildcardQuery
				{
					Field = "my_other_prefix_field", 
					Value = "value"
				};
			var searchRequest = new SearchRequest()
			{
				Query = boolQuery
			};

			var search = this._client.Search<ElasticsearchProject>(searchRequest);

			var request = search.RequestInformation.Request.Utf8String();
			request.Should().Contain("name");
			request.Should().Contain("my_other_prefix_field");
			Assert.Pass(request);
		}
	}
}