// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.DanglingIndices.Delete
{
	public class DeleteDanglingIndexApiTests
		: ApiTestBase<ReadOnlyCluster, DeleteDanglingIndexResponse, IDeleteDanglingIndexRequest, DeleteDanglingIndexDescriptor, DeleteDanglingIndexRequest>
	{
		private static readonly string IndexUuid = "indexuuid";

		public DeleteDanglingIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<DeleteDanglingIndexDescriptor, IDeleteDanglingIndexRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteDanglingIndexRequest Initializer => new DeleteDanglingIndexRequest(IndexUuid);
		protected override string UrlPath => $"/_dangling/{IndexUuid}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DanglingIndices.DeleteDanglingIndex(IndexUuid, f),
			(client, f) => client.DanglingIndices.DeleteDanglingIndexAsync(IndexUuid, f),
			(client, r) => client.DanglingIndices.DeleteDanglingIndex(r),
			(client, r) => client.DanglingIndices.DeleteDanglingIndexAsync(r)
		);

		protected override DeleteDanglingIndexDescriptor NewDescriptor() => new DeleteDanglingIndexDescriptor(IndexUuid);
	}
}
