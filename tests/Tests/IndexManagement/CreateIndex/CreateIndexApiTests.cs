// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Transport;
using FluentAssertions;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.IndexManagement.CreateIndex;

public class CreateBasicIndexApiTests
	: ApiIntegrationTestBase<WritableCluster, CreateIndexResponse, CreateIndexRequestDescriptor,
		CreateIndexRequest>
{
	public CreateBasicIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;
	protected override HttpMethod HttpMethod => HttpMethod.PUT;

	protected override CreateIndexRequest Initializer => new(CallIsolatedValue);

	protected override string ExpectedUrlPathAndQuery => $"/{CallIsolatedValue}";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.IndexManagement.CreateIndex(CallIsolatedValue, f),
		(client, f) => client.IndexManagement.CreateIndexAsync(CallIsolatedValue, f),
		(client, r) => client.IndexManagement.CreateIndex(r),
		(client, r) => client.IndexManagement.CreateIndexAsync(r)
	);

	protected override CreateIndexRequestDescriptor NewDescriptor() => new(CallIsolatedValue);

	protected override void ExpectResponse(CreateIndexResponse response)
	{
		response.ShouldBeValid();
		response.Acknowledged.Should().BeTrue();
		response.ShardsAcknowledged.Should().BeTrue();
		response.Index.Should().Be(CallIsolatedValue);
	}
}

//public class CreateIndexWithSettingsApiTests
//	: ApiIntegrationTestBase<WritableCluster, CreateIndexResponse, CreateIndexRequestDescriptor,
//		CreateIndexRequest>
//{
//	public CreateIndexWithSettingsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

//	protected override bool ExpectIsValid => true;
//	protected override int ExpectStatusCode => 200;
//	protected override HttpMethod HttpMethod => HttpMethod.PUT;

//	protected override CreateIndexRequest Initializer => new(CallIsolatedValue);

//	protected override Action<CreateIndexRequestDescriptor> Fluent => f => f.Settings(s => s.Add("analysis", new IndexSettingsAnalysisDescriptor().TokenFilters(tf => tf.Shingle("my-shingle", s=> s.MinShingleSize(2)))));

//	protected override string ExpectedUrlPathAndQuery => $"/{CallIsolatedValue}";

//	protected override LazyResponses ClientUsage() => Calls(
//		(client, f) => client.IndexManagement.CreateIndex(CallIsolatedValue, f),
//		(client, f) => client.IndexManagement.CreateIndexAsync(CallIsolatedValue, f),
//		(client, r) => client.IndexManagement.CreateIndex(r),
//		(client, r) => client.IndexManagement.CreateIndexAsync(r)
//	);

//	protected override CreateIndexRequestDescriptor NewDescriptor() => new(CallIsolatedValue);

//	protected override void ExpectResponse(CreateIndexResponse response)
//	{
//		response.ShouldBeValid();
//		response.Acknowledged.Should().BeTrue();
//		response.ShardsAcknowledged.Should().BeTrue();
//		response.Index.Should().Be(CallIsolatedValue);
//	}
//}
