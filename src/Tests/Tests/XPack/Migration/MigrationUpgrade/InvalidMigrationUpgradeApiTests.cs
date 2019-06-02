using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Migration.MigrationUpgrade
{
	[SkipVersion("<6.3.0", "Not running on latest version")]
	public class InvalidMigrationUpgradeApiTests
		: ApiIntegrationTestBase<XPackCluster, MigrationUpgradeResponse, IMigrationUpgradeRequest, MigrationUpgradeDescriptor,
			MigrationUpgradeRequest>
	{
		public InvalidMigrationUpgradeApiTests(XPackCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 400;

		protected override Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override MigrationUpgradeRequest Initializer => new MigrationUpgradeRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_migration/upgrade/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Migration.Upgrade(CallIsolatedValue, f),
			(c, f) => c.Migration.UpgradeAsync(CallIsolatedValue, f),
			(c, r) => c.Migration.Upgrade(r),
			(c, r) => c.Migration.UpgradeAsync(r)
		);

		protected override MigrationUpgradeDescriptor NewDescriptor() => new MigrationUpgradeDescriptor(CallIsolatedValue);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var (k, v) in values)
				client.Indices.Create(v);
		}
		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var (k, v) in values)
				client.Indices.Delete(v);
		}

		protected override void ExpectResponse(MigrationUpgradeResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(400);
		}
	}
}
