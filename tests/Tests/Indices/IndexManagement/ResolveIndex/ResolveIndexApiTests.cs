// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexManagement.ResolveIndex
{
	[SkipVersion("<7.9.0", "resolve index introduced in 7.9.0")]
	public class ResolveIndexApiTests
		: ApiIntegrationTestBase<WritableCluster, ResolveIndexResponse, IResolveIndexRequest, ResolveIndexDescriptor, ResolveIndexRequest>
	{
		public ResolveIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var value in values)
			{
				var createIndexResponse = client.Indices.Create(value.Value, c => c
					.Settings(s => s
						.NumberOfShards(1)
						.NumberOfReplicas(0)
					)
					.Aliases(a => a
						.Alias(value.Value + "-alias")
					)
				);

				if (!createIndexResponse.IsValid)
					throw new Exception($"exception whilst setting up integration test: {createIndexResponse.DebugInformation}");

				var clusterResponse = client.Cluster.Health(value.Value, c => c.WaitForStatus(WaitForStatus.Green));

				if (!clusterResponse.IsValid)
					throw new Exception($"exception whilst setting up integration test: {clusterResponse.DebugInformation}");
			}
		}

		protected override bool ExpectIsValid => true;

		protected override int ExpectStatusCode => 200;

		protected override Func<ResolveIndexDescriptor, IResolveIndexRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override ResolveIndexRequest Initializer => new ResolveIndexRequest($"{CallIsolatedValue}*");

		protected override string UrlPath => $"/_resolve/index/{CallIsolatedValue}%2A";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Resolve($"{CallIsolatedValue}*", f),
			(client, f) => client.Indices.ResolveAsync($"{CallIsolatedValue}*", f),
			(client, r) => client.Indices.Resolve(r),
			(client, r) => client.Indices.ResolveAsync(r)
		);

		protected override ResolveIndexDescriptor NewDescriptor() => new ResolveIndexDescriptor($"{CallIsolatedValue}*");

		protected override void ExpectResponse(ResolveIndexResponse response)
		{
			response.ShouldBeValid();
			response.Indices.Should().HaveCount(1);
			var resolvedIndex = response.Indices.First();
			resolvedIndex.Name.Should().Be(CallIsolatedValue);
			resolvedIndex.Aliases.Should().Contain(CallIsolatedValue + "-alias");
			resolvedIndex.Attributes.Should().Contain("open");
			var resolvedAlias = response.Aliases.First();
			resolvedAlias.Name.Should().Be(CallIsolatedValue + "-alias");
			resolvedAlias.Indices.Should().Contain(CallIsolatedValue);
		}
	}
}
