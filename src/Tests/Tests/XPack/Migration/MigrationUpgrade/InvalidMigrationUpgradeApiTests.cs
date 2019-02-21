using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Migration.MigrationUpgrade
{
	[SkipVersion("<6.3.0", "Not running on latest version")]
	public class InvalidMigrationUpgradeApiTests
		: ApiIntegrationTestBase<XPackCluster, IMigrationUpgradeResponse, IMigrationUpgradeRequest, MigrationUpgradeDescriptor,
			MigrationUpgradeRequest>
	{
		public InvalidMigrationUpgradeApiTests(XPackCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 500;

		protected override Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override MigrationUpgradeRequest Initializer => new MigrationUpgradeRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_migration/upgrade/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.MigrationUpgrade(CallIsolatedValue, f),
			(c, f) => c.MigrationUpgradeAsync(CallIsolatedValue, f),
			(c, r) => c.MigrationUpgrade(r),
			(c, r) => c.MigrationUpgradeAsync(r)
		);

		protected override MigrationUpgradeDescriptor NewDescriptor() => new MigrationUpgradeDescriptor(CallIsolatedValue);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var (k, v) in values)
				client.CreateIndex(v);
		}
		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var (k, v) in values)
				client.DeleteIndex(v);
		}

		protected override void ExpectResponse(IMigrationUpgradeResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(500);
		}
	}
}
