using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.DeleteByQuery
{
	[TestFixture]
	public class DeleteByQueryRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public DeleteByQueryRequestTests()
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

			var request = new DeleteByQueryRequest()
			{
				AllowNoIndices = true,
				ExpandWildcards = ExpandWildcards.Closed,
				Query = query,
				DefaultOperator = DefaultOperator.And
			};
			var response = this._client.DeleteByQuery(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_all/_query?allow_no_indices=true&expand_wildcards=closed&default_operator=AND");
			this._status.RequestMethod.Should().Be("DELETE");
		}
		
		[Test]
		public void DeleteByQueryBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}
}
