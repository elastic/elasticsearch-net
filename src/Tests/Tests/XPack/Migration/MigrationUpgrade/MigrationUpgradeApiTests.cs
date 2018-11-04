using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Migration.MigrationUpgrade
{
	[SkipVersion("<5.6.0", "Introduced in Elasticsearch 5.6.0 to aid in upgrading")]
	public class MigrationUpgradeApiTests
		: ApiIntegrationTestBase<XPackCluster, IMigrationUpgradeResponse, IMigrationUpgradeRequest, MigrationUpgradeDescriptor,
			MigrationUpgradeRequest>
	{
		public MigrationUpgradeApiTests(XPackCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 500;

		protected override Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override MigrationUpgradeRequest Initializer => new MigrationUpgradeRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_xpack/migration/upgrade/{CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var createIndexResponse = client.CreateIndex(callUniqueValue.Value, c => c
					.Settings(s => s
						.NumberOfShards(1)
						.NumberOfReplicas(0)
					)
					.Mappings(m => m
						.Map<Project>(mm => mm.AutoMap())
					)
				);

				if (!createIndexResponse.IsValid)
					throw new Exception($"problem creating index for integration test: {createIndexResponse.DebugInformation}");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.MigrationUpgrade(CallIsolatedValue, f),
			(c, f) => c.MigrationUpgradeAsync(CallIsolatedValue, f),
			(c, r) => c.MigrationUpgrade(r),
			(c, r) => c.MigrationUpgradeAsync(r)
		);

		protected override MigrationUpgradeDescriptor NewDescriptor() => new MigrationUpgradeDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IMigrationUpgradeResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().NotBeNull();
		}
	}
}
