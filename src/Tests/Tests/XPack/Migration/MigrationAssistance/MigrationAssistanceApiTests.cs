using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Migration.MigrationAssistance
{
	[SkipVersion("<5.6.0", "Introduced in Elasticsearch 5.6.0 to aid in upgrading")]
	public class MigrationAssistanceApiTests
		: ApiIntegrationTestBase<XPackCluster, IMigrationAssistanceResponse, IMigrationAssistanceRequest, MigrationAssistanceDescriptor,
			MigrationAssistanceRequest>
	{
		public MigrationAssistanceApiTests(XPackCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> Fluent => f => f
			.Index(CallIsolatedValue);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override MigrationAssistanceRequest Initializer => new MigrationAssistanceRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_xpack/migration/assistance/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.MigrationAssistance(f),
			(c, f) => c.MigrationAssistanceAsync(f),
			(c, r) => c.MigrationAssistance(r),
			(c, r) => c.MigrationAssistanceAsync(r)
		);

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

				client.ClusterHealth(c => c.Index(callUniqueValue.Value).WaitForStatus(WaitForStatus.Green));
			}
		}

		protected override MigrationAssistanceDescriptor NewDescriptor() => new MigrationAssistanceDescriptor();

		protected override void ExpectResponse(IMigrationAssistanceResponse response)
		{
			response.ShouldBeValid();
			response.ServerError.Should().BeNull();
			response.Indices.Should().NotBeNull().And.BeEmpty();
		}
	}

	public class MigrationAssistanceSerializationTests
	{
		[U] public void ShouldDeserialize()
		{
			var fixedResponse = new
			{
				indices = new Dictionary<string, object>()
				{
					{ ".watches", new { action_required = "upgrade" } },
					{ ".security", new { action_required = "upgrade" } },
					{ "my_old_index", new { action_required = "reindex" } },
					{ "my_other_old_index", new { action_required = "reindex" } },
				}
			};

			var client = FixedResponseClient.Create(fixedResponse);

			var response = client.MigrationAssistance();
			response.ShouldBeValid();
			response.Indices.Should().NotBeNull().And.HaveCount(4);

			foreach (var index in response.Indices)
			{
				index.Value.Should().NotBeNull();
				if (index.Key.Name == ".watches" || index.Key.Name == ".security")
					index.Value.ActionRequired.Should().Be(UpgradeActionRequired.Upgrade);
				else
					index.Value.ActionRequired.Should().Be(UpgradeActionRequired.Reindex);
			}
		}
	}
}
