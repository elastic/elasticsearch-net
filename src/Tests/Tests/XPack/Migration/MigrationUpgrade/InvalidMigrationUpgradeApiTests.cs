using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Migration.MigrationUpgrade
{
	public class InvalidMigrationUpgradeApiTests
		: ApiIntegrationTestBase<XPackCluster, IMigrationUpgradeResponse, IMigrationUpgradeRequest, MigrationUpgradeDescriptor,
			MigrationUpgradeRequest>
	{
		private const string index = ".watches";

		public InvalidMigrationUpgradeApiTests(XPackCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 400;

		protected override Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override MigrationUpgradeRequest Initializer => new MigrationUpgradeRequest(index);

		protected override string UrlPath => $"/_xpack/migration/upgrade/{index}";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.MigrationUpgrade(index, f),
			(c, f) => c.MigrationUpgradeAsync(index, f),
			(c, r) => c.MigrationUpgrade(r),
			(c, r) => c.MigrationUpgradeAsync(r)
		);

		protected override MigrationUpgradeDescriptor NewDescriptor() => new MigrationUpgradeDescriptor(index);

		protected override void ExpectResponse(IMigrationUpgradeResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().BeNull();
		}
	}
}
