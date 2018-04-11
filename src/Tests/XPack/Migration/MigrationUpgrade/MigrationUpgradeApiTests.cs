using System;
using System.Collections.Generic;
using System.Net;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.Migration.MigrationUpgrade
{
	[SkipVersion("<5.6.0","Introduced in Elasticsearch 5.6.0 to aid in upgrading")]
	public class MigrationUpgradeApiTests :
		ApiIntegrationTestBase<XPackCluster, IMigrationUpgradeResponse, IMigrationUpgradeRequest, MigrationUpgradeDescriptor, MigrationUpgradeRequest>
	{
		public MigrationUpgradeApiTests(XPackCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

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
			fluent: (c, f) => c.MigrationUpgrade(CallIsolatedValue, f),
			fluentAsync: (c, f) => c.MigrationUpgradeAsync(CallIsolatedValue, f),
			request: (c, r) => c.MigrationUpgrade(r),
			requestAsync: (c, r) => c.MigrationUpgradeAsync(r)
		);

		protected override string UrlPath => $"/_xpack/migration/upgrade/{CallIsolatedValue}";
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override int ExpectStatusCode => 500;
		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => null;

		protected override MigrationUpgradeDescriptor NewDescriptor() => new MigrationUpgradeDescriptor(CallIsolatedValue);

		protected override Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> Fluent => f => f;

		protected override MigrationUpgradeRequest Initializer => new MigrationUpgradeRequest(CallIsolatedValue);

		protected override void ExpectResponse(IMigrationUpgradeResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().NotBeNull();
		}
	}
}
