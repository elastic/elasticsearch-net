using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Count
{
	[TestFixture]
	public class CountRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public CountRequestTests()
		{
			QueryContainer query = new TermQuery()
			{
				Field = Property.Path<ElasticsearchProject>(p=>p.Name),
				Value = "value"
			} && new PrefixQuery()
			{
				Field = "prefix_field", 
				Value = "prefi", 
				Rewrite = RewriteMultiTerm.ConstantScoreBoolean
			};

			var request = new CountRequest()
			{
				AllowNoIndices = true,
				ExpandWildcards = ExpandWildcards.Closed,
				MinScore = 0.6,
				Query = query
			};
			var response = this._client.Count<ElasticsearchProject>(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_count?allow_no_indices=true&expand_wildcards=closed&min_score=0.6");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void CountBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}
}
