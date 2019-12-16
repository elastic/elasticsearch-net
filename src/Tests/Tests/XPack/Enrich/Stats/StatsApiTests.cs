using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.Enrich.Stats
{
	[SkipVersion("<7.5.0", "Introduced in 7.5.0")]
	public class EnrichStatsApiTests
		: ApiIntegrationTestBase<XPackCluster, EnrichStatsResponse, IEnrichStatsRequest, EnrichStatsDescriptor, EnrichStatsRequest>
	{
		public EnrichStatsApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => GET;

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putPolicyResponse = client.Enrich.PutPolicy<Project>(callUniqueValue.Value, p => p
					.Match(m => m
						.Indices(typeof(Project))
						.MatchField(f => f.Name)
						.EnrichFields(f => f
							.Field(ff => ff.Description)
							.Field(ff => ff.Tags)
						)
					)
				);

				if (!putPolicyResponse.IsValid)
					throw new Exception($"Failure setting up integration test: {putPolicyResponse.DebugInformation}");
			}
		}

		protected override void OnBeforeCall(IElasticClient client)
		{
			var executePolicyResponse = client.Enrich.ExecutePolicy(CallIsolatedValue, e => e.WaitForCompletion(false));

			if (!executePolicyResponse.IsValid)
				throw new Exception($"Failure setting up integration test: {executePolicyResponse.DebugInformation}");
		}

		protected override string UrlPath => $"/_enrich/_stats";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Enrich.Stats(f),
			(client, f) => client.Enrich.StatsAsync(f),
			(client, r) => client.Enrich.Stats(r),
			(client, r) => client.Enrich.StatsAsync(r)
		);

		protected override void ExpectResponse(EnrichStatsResponse response)
		{
			response.ExecutingPolicies.Should().NotBeNull();
			response.CoordinatorStats.Should().NotBeNull();
		}
	}
}
