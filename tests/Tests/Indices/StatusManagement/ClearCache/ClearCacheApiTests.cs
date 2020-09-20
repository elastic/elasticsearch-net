// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Indices.StatusManagement.ClearCache
{
	public class ClearCacheApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ClearCacheResponse, IClearCacheRequest, ClearCacheDescriptor, ClearCacheRequest>
	{
		public ClearCacheApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<ClearCacheDescriptor, IClearCacheRequest> Fluent => d => d.Request();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ClearCacheRequest Initializer => new ClearCacheRequest(AllIndices) { Request = true };
		protected override string UrlPath => "/_all/_cache/clear?request=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.ClearCache(AllIndices, f),
			(client, f) => client.Indices.ClearCacheAsync(AllIndices, f),
			(client, r) => client.Indices.ClearCache(r),
			(client, r) => client.Indices.ClearCacheAsync(r)
		);
	}
}
