using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.SnapshotAndRestore.Repositories.DeleteRepository
{
	public class DeleteRepositoryApiTests
		: ApiTestBase<ReadOnlyCluster, IDeleteRepositoryResponse, IDeleteRepositoryRequest, DeleteRepositoryDescriptor, DeleteRepositoryRequest>
	{
		private static readonly string _name = "repository1";

		public DeleteRepositoryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteRepositoryRequest Initializer => new DeleteRepositoryRequest(_name);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteRepository(_name, f),
			(client, f) => client.DeleteRepositoryAsync(_name, f),
			(client, r) => client.DeleteRepository(r),
			(client, r) => client.DeleteRepositoryAsync(r)
		);

		protected override DeleteRepositoryDescriptor NewDescriptor() => new DeleteRepositoryDescriptor(_name);
	}
}
