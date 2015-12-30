using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Repositories.DeleteRepository
{
	[Collection(IntegrationContext.ReadOnly)]
	public class DeleteRepositoryApiTests : ApiTestBase<IDeleteRepositoryResponse, IDeleteRepositoryRequest, DeleteRepositoryDescriptor, DeleteRepositoryRequest>
	{
		public DeleteRepositoryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _name = "repository1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteRepository(_name, f),
			fluentAsync: (client, f) => client.DeleteRepositoryAsync(_name, f),
			request: (client, r) => client.DeleteRepository(r),
			requestAsync: (client, r) => client.DeleteRepositoryAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override bool SupportsDeserialization => false;

		protected override DeleteRepositoryDescriptor NewDescriptor() => new DeleteRepositoryDescriptor(_name);

		protected override Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> Fluent => d => d;

		protected override DeleteRepositoryRequest Initializer => new DeleteRepositoryRequest(_name);
	}
}
