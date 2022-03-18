// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Single.Exists
{
	public class DocumentExistsApiTests
		: ApiIntegrationTestBase<WritableCluster, ExistsResponse, ExistsRequestDescriptor, ExistsRequest>
	{
		public DocumentExistsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;

		protected override string ExpectedUrlPathAndQuery => $"/project/_doc/{CallIsolatedValue}?routing={CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;
		protected override ExistsRequestDescriptor NewDescriptor() => new(Infer.Index<Project>(), CallIsolatedValue);

		protected override void IntegrationSetup(ElasticsearchClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id).Routing(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Exists(Infer.Index<Project>(), CallIsolatedValue, f),
			(client, f) => client.ExistsAsync(Infer.Index<Project>(), CallIsolatedValue, f),
			(client, r) => client.Exists(r),
			(client, r) => client.ExistsAsync(r)
		);

		protected override ExistsRequest Initializer => new(Infer.Index<Project>(), CallIsolatedValue) { Routing = CallIsolatedValue };
		protected override Action<ExistsRequestDescriptor> Fluent => d => d.Routing(CallIsolatedValue);

		protected override void ExpectResponse(ExistsResponse response)
		{
			response.Should().NotBeNull();
			response.Exists.Should().BeTrue();
		}
	}
}
