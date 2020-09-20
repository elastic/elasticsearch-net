// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Repositories.DeleteRepository
{
	public class DeleteRepositoryApiTests
		: ApiTestBase<ReadOnlyCluster, DeleteRepositoryResponse, IDeleteRepositoryRequest, DeleteRepositoryDescriptor, DeleteRepositoryRequest>
	{
		private static readonly string _name = "repository1";

		public DeleteRepositoryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteRepositoryRequest Initializer => new DeleteRepositoryRequest(_name);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.DeleteRepository(_name, f),
			(client, f) => client.Snapshot.DeleteRepositoryAsync(_name, f),
			(client, r) => client.Snapshot.DeleteRepository(r),
			(client, r) => client.Snapshot.DeleteRepositoryAsync(r)
		);

		protected override DeleteRepositoryDescriptor NewDescriptor() => new DeleteRepositoryDescriptor(_name);
	}
}
