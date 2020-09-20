// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Configuration;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Ingest.PutPipeline
{
	[SkipVersion("<7.8.0", "Empty Value bug in versions less than Elasticsearch 7.8.0")]
	public class PutPipelineApiTests
		: ApiIntegrationTestBase<XPackCluster, PutPipelineResponse, IPutPipelineRequest, PutPipelineDescriptor, PutPipelineRequest>
	{
		private static readonly string _id = "pipeline-1";

		public PutPipelineApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			description = "My test pipeline",
			processors = ProcessorAssertions.AllAsJson
		};

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			if (TestConfiguration.Instance.InRange(">=7.5.0"))
			{
				var putPolicyResponse = client.Enrich.PutPolicy<Project>(ProcessorAssertions.Enrich.PolicyName, p => p
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

				var executePolicyResponse = client.Enrich.ExecutePolicy(ProcessorAssertions.Enrich.PolicyName);

				if (!executePolicyResponse.IsValid)
					throw new Exception($"Failure setting up integration test: {executePolicyResponse.DebugInformation}");
			}
		}

		protected override int ExpectStatusCode => 200;

		protected override Func<PutPipelineDescriptor, IPutPipelineRequest> Fluent => d => d
			.Description("My test pipeline")
			.Processors(ProcessorAssertions.Fluent);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutPipelineRequest Initializer => new PutPipelineRequest(_id)
		{
			Description = "My test pipeline",
			Processors = ProcessorAssertions.Initializers
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_ingest/pipeline/{_id}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Ingest.PutPipeline(_id, f),
			(client, f) => client.Ingest.PutPipelineAsync(_id, f),
			(client, r) => client.Ingest.PutPipeline(r),
			(client, r) => client.Ingest.PutPipelineAsync(r)
		);

		protected override PutPipelineDescriptor NewDescriptor() => new PutPipelineDescriptor(_id);

		protected override void ExpectResponse(PutPipelineResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
