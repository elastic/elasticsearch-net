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

namespace Tests.XPack.Enrich.ExecutePolicy
{
	[SkipVersion("<7.5.0", "Introduced in 7.5.0")]
	public class ExecutePolicyApiTests
		: ApiIntegrationTestBase<XPackCluster, ExecuteEnrichPolicyResponse, IExecuteEnrichPolicyRequest, ExecuteEnrichPolicyDescriptor, ExecuteEnrichPolicyRequest>
	{
		public ExecutePolicyApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => PUT;

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

		protected override ExecuteEnrichPolicyRequest Initializer => new ExecuteEnrichPolicyRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_enrich/policy/{CallIsolatedValue}/_execute";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Enrich.ExecutePolicy(CallIsolatedValue, f),
			(client, f) => client.Enrich.ExecutePolicyAsync(CallIsolatedValue, f),
			(client, r) => client.Enrich.ExecutePolicy(r),
			(client, r) => client.Enrich.ExecutePolicyAsync(r)
		);

		protected override void ExpectResponse(ExecuteEnrichPolicyResponse response) =>
			response.Status.Phase.Should().Be(EnrichPolicyPhase.Complete);
	}
}
