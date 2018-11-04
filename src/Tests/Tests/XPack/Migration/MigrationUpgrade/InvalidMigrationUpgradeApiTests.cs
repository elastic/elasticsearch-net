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
	[SkipVersion(">=6.3.0", "Not running on latest version")]
	public class InvalidMigrationUpgradeApiTests
		: ApiIntegrationTestBase<XPackCluster, IMigrationUpgradeResponse, IMigrationUpgradeRequest, MigrationUpgradeDescriptor,
			MigrationUpgradeRequest>
	{
		private const string Index = ".watches";

		public InvalidMigrationUpgradeApiTests(XPackCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 400;

		protected override Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override MigrationUpgradeRequest Initializer => new MigrationUpgradeRequest(Index);

		protected override string UrlPath => $"/_xpack/migration/upgrade/{Index}";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.MigrationUpgrade(Index, f),
			(c, f) => c.MigrationUpgradeAsync(Index, f),
			(c, r) => c.MigrationUpgrade(r),
			(c, r) => c.MigrationUpgradeAsync(r)
		);

		protected override MigrationUpgradeDescriptor NewDescriptor() => new MigrationUpgradeDescriptor(Index);

		protected override void ExpectResponse(IMigrationUpgradeResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().BeNull();
		}
	}
}
