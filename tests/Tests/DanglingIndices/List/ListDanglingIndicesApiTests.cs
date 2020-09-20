// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.DanglingIndices.List
{
	public class ListDanglingIndicesApiTests
		: ApiTestBase<ReadOnlyCluster, ListDanglingIndicesResponse, IListDanglingIndicesRequest, ListDanglingIndicesDescriptor, ListDanglingIndicesRequest>
	{
		public ListDanglingIndicesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<ListDanglingIndicesDescriptor, IListDanglingIndicesRequest> Fluent => d => d;

		protected override ListDanglingIndicesRequest Initializer => new ListDanglingIndicesRequest();

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"/_dangling";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DanglingIndices.List(f),
			(client, f) => client.DanglingIndices.ListAsync(f),
			(client, r) => client.DanglingIndices.List(r),
			(client, r) => client.DanglingIndices.ListAsync(r)
		);
	}
}
