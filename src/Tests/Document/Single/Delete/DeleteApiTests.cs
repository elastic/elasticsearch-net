using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Delete
{
	[Collection(IntegrationContext.Indexing)]
	public class DeleteApiTests : ApiIntegrationTestBase<IDeleteResponse, IDeleteRequest, DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void BeforeAllCalls(IElasticClient client, IDictionary<ClientMethod, string> values)
		{
			foreach (var id in values.Values)
				this.Client.Index(Project.Instance, i=>i.Id(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Delete<Project>(CallIsolatedValue),
			fluentAsync: (client, f) => client.DeleteAsync<Project>(CallIsolatedValue),
			request: (client, r) => client.Delete(r),
			requestAsync: (client, r) => client.DeleteAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/project/project/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;

		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => null;
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue);
	}
}
