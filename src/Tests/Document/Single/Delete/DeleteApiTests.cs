using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Delete<Project>(Project.Instance),
			fluentAsync: (client, f) => client.DeleteAsync<Project>(Project.Instance),
			request: (client, r) => client.Delete(r),
			requestAsync: (client, r) => client.DeleteAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/project/project/{Project.Instance.Name}";

		protected override bool SupportsDeserialization => false;

		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => null;
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(Project.Instance);

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
