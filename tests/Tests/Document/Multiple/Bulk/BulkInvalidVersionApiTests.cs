// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple;

public class BulkInvalidVersionApiTests : ApiIntegrationTestBase<WritableCluster, BulkResponse, BulkRequestDescriptor, BulkRequest>
{
	public BulkInvalidVersionApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => false;

	protected override int ExpectStatusCode => 400;

	protected override Action<BulkRequestDescriptor> Fluent => d => d
		.Index(CallIsolatedValue)
		.Index(Project.Instance)
		.Index(Project.Instance, i => i.IfSequenceNumber(-1).IfPrimaryTerm(0));

	protected override HttpMethod HttpMethod => HttpMethod.POST;

	protected override BulkRequest Initializer => new(CallIsolatedValue)
	{
		Operations = new List<IBulkOperation>
		{
			new BulkIndexOperation<Project>(Project.Instance),
			new BulkIndexOperation<Project>(Project.Instance) { IfSequenceNumber = -1, IfPrimaryTerm = 0 }
		}
	};

	protected override bool SupportsDeserialization => false;

	protected override string ExpectedUrlPathAndQuery => $"/{CallIsolatedValue}/_bulk";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Bulk(f),
		(client, f) => client.BulkAsync(f),
		(client, r) => client.Bulk(r),
		(client, r) => client.BulkAsync(r)
	);

	protected override void ExpectResponse(BulkResponse response)
	{
		response.ShouldNotBeValid();
		response.ServerError.Should().NotBeNull();
		response.ServerError.Status.Should().Be(400);
		response.ServerError.Error.Type.Should().Be("illegal_argument_exception");
		response.ServerError.Error.Reason.Should().StartWith("sequence numbers must be non negative.");
	}
}
