using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class BoolQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void Bool_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Bool,
				f=>f.Bool(b=>b
					.Must(Query1)
					.Should(Query2)
					.MustNot(Query3)
					.Boost(1.1)
					.DisableCoord()
					.MinimumShouldMatch(2)
				)
			);

			q.Boost.Should().Be(1.1);
			q.DisableCoord.Should().BeTrue();
			q.MinimumShouldMatch.Should().Be("2");
			q.Must.Should().NotBeEmpty().And.HaveCount(1);
			q.Should.Should().NotBeEmpty().And.HaveCount(1);
			q.MustNot.Should().NotBeEmpty().And.HaveCount(1);

			AssertIsTermQuery(q.Must.First(), Query1);
			AssertIsTermQuery(q.Should.First(), Query2);
			AssertIsTermQuery(q.MustNot.First(), Query3);
		}

		[Test]
		public void Bool_PlainSyntax()
		{
			QueryContainer wildcardQuery = new BoolQuery
			{
				Must = new List<QueryContainer>
				{
					new WildcardQuery() { Field = "myprefix_field", Value = "value"},
					new WildcardQuery() { Field = "my_prefix_field", Value = "value"}
				}
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

	}
}