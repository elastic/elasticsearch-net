using System;
using System.Collections.Generic;
using System.Net;
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
	public class InvalidMigrationUpgradeApiTests :
		ApiIntegrationTestBase<XPackCluster, IMigrationUpgradeResponse, IMigrationUpgradeRequest, MigrationUpgradeDescriptor, MigrationUpgradeRequest>
	{
		public InvalidMigrationUpgradeApiTests(XPackCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.MigrationUpgrade(index, f),
			fluentAsync: (c, f) => c.MigrationUpgradeAsync(index, f),
			request: (c, r) => c.MigrationUpgrade(r),
			requestAsync: (c, r) => c.MigrationUpgradeAsync(r)
		);

		private const string index = ".watches";

		protected override string UrlPath => $"/_xpack/migration/upgrade/{index}";
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override int ExpectStatusCode => 400;
		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => null;

		protected override MigrationUpgradeDescriptor NewDescriptor() => new MigrationUpgradeDescriptor(index);

		protected override Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> Fluent => f => f;

		protected override MigrationUpgradeRequest Initializer => new MigrationUpgradeRequest(index);

		protected override void ExpectResponse(IMigrationUpgradeResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().BeNull();
		}
	}
}
