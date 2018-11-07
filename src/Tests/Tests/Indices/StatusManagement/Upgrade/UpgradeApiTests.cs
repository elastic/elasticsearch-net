using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.StatusManagement.Upgrade
{
	[SkipVersion("<=5.0.0", "AllowNoIndices() only available from 5.0.1 onwards")]
	public class UpgradeApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<IntrusiveOperationCluster, IUpgradeResponse, IUpgradeRequest, UpgradeDescriptor, UpgradeRequest>
	{
		public UpgradeApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<UpgradeDescriptor, IUpgradeRequest> Fluent => d => d.AllowNoIndices();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override UpgradeRequest Initializer => new UpgradeRequest(CallIsolatedValue) { AllowNoIndices = true };
		protected override string UrlPath => $"/{CallIsolatedValue}/_upgrade?allow_no_indices=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Upgrade(CallIsolatedValue, f),
			(client, f) => client.UpgradeAsync(CallIsolatedValue, f),
			(client, r) => client.Upgrade(r),
			(client, r) => client.UpgradeAsync(r)
		);
	}
}
