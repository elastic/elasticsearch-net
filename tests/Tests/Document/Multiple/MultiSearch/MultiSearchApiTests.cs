// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.MultiGet;

public class MultiSearchApiTests
	: ApiIntegrationTestBase<ReadOnlyCluster, MultiSearchResponse<Developer>, MultiSearchRequestDescriptor<Developer>, MultiSearchRequest>
{
	private readonly IEnumerable<long> _ids = Developer.Developers.Select(d => d.Id).Take(10);

	public MultiSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;

	protected override bool VerifyJson => true;

	protected override int ExpectStatusCode => 200;

	protected override Action<MultiSearchRequestDescriptor<Developer>> Fluent => d => { }; // TODO

	protected override HttpMethod HttpMethod => HttpMethod.POST;

	protected override MultiSearchRequest Initializer => new(Infer.Index<Developer>()) // TODO
	{
	};

	protected override bool SupportsDeserialization => false;

	protected override string ExpectedUrlPathAndQuery => "/devs/_mget";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.MultiSearch(f),
		(client, f) => client.MultiSearchAsync(f),
		(client, r) => client.MultiSearch<Developer>(r),
		(client, r) => client.MultiSearchAsync<Developer>(r)
	);

	protected override void ExpectResponse(MultiSearchResponse<Developer> response)
	{
		// TODO
	}
}
