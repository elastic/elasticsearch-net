using System;
using System.Collections.Generic;
using System.Net;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.Migration.MigrationUpgrade
{
	[SkipVersion(">=6.3.0", "Not running on latest version")]
	public class InvalidMigrationUpgradeApiTests :
		ApiIntegrationTestBase<XPackCluster, IMigrationUpgradeResponse, IMigrationUpgradeRequest, MigrationUpgradeDescriptor, MigrationUpgradeRequest>
	{
		public InvalidMigrationUpgradeApiTests(XPackCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.MigrationUpgrade(Index, f),
			fluentAsync: (c, f) => c.MigrationUpgradeAsync(Index, f),
			request: (c, r) => c.MigrationUpgrade(r),
			requestAsync: (c, r) => c.MigrationUpgradeAsync(r)
		);

		private const string Index = ".watches";

		protected override string UrlPath => $"/_xpack/migration/upgrade/{Index}";
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override int ExpectStatusCode => 400;
		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => null;

		protected override MigrationUpgradeDescriptor NewDescriptor() => new MigrationUpgradeDescriptor(Index);

		protected override Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> Fluent => f => f;

		protected override MigrationUpgradeRequest Initializer => new MigrationUpgradeRequest(Index);

		protected override void ExpectResponse(IMigrationUpgradeResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().BeNull();
		}
	}
}
