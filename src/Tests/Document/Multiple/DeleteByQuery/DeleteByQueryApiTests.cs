using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Static;

namespace Tests.Document.Multiple.DeleteByQuery
{
	[Collection(IntegrationContext.Indexing)]
	public class DeleteByQueryApiTests : ApiTestBase<IDeleteByQueryResponse, IDeleteByQueryRequest, DeleteByQueryDescriptor<Project>, DeleteByQueryRequest>
	{
		public DeleteByQueryApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteByQuery(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.DeleteByQueryAsync(CallIsolatedValue, f),
			request: (client, r) => client.DeleteByQuery(r),
			requestAsync: (client, r) => client.DeleteByQueryAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/{CallIsolatedValue}/_query";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new 
		{
			query = new
			{
				match_all = new { }
			}
		};

		protected override DeleteByQueryDescriptor<Project> NewDescriptor() => new DeleteByQueryDescriptor<Project>(CallIsolatedValue);

		protected override Func<DeleteByQueryDescriptor<Project>, IDeleteByQueryRequest> Fluent => d => d
			.MatchAll();
			
		protected override DeleteByQueryRequest Initializer => new DeleteByQueryRequest(CallIsolatedValue)
		{
			Query = new MatchAllQuery()
		};
	}
}
