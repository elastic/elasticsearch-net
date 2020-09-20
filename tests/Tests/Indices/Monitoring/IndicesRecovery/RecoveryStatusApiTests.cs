// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.Monitoring.IndicesRecovery
{
	public class RecoveryStatusApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, RecoveryStatusResponse, IRecoveryStatusRequest, RecoveryStatusDescriptor, RecoveryStatusRequest>
	{
		public RecoveryStatusApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override RecoveryStatusRequest Initializer => new RecoveryStatusRequest(Infer.AllIndices);
		protected override string UrlPath => "/_all/_recovery";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.RecoveryStatus(Infer.AllIndices, f),
			(client, f) => client.Indices.RecoveryStatusAsync(Infer.AllIndices, f),
			(client, r) => client.Indices.RecoveryStatus(r),
			(client, r) => client.Indices.RecoveryStatusAsync(r)
		);
	}
}
