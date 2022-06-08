// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.MultiGet;

public class MultiSearchApiTests
	: ApiIntegrationTestBase<ReadOnlyCluster, MultiSearchResponse<Developer>, MultiSearchRequestDescriptor<Developer>, MultiSearchRequest>
{
	public MultiSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;

	protected override bool VerifyJson => true;

	protected override int ExpectStatusCode => 200;

	// TODO - Fluent API improvements after POC code-gen
	protected override Action<MultiSearchRequestDescriptor<Developer>> Fluent => d => d
		.Indices(Infer.Index<Project>()) // TODO - Should support fluent verion and ctor with Indices
		.AddSearch(new RequestItem(new MultisearchBody { From = 0, Size = 10, Query = new MatchAllQuery() }))
		.AddSearch(new RequestItem(new MultisearchBody { From = 0, Size = 1, Query = new MatchAllQuery() }));

	protected override HttpMethod HttpMethod => HttpMethod.POST;

	protected override MultiSearchRequest Initializer => new(Infer.Index<Project>())
	{
		Searches = new List<RequestItem>
		{
			new RequestItem(new MultisearchBody { From = 0, Size = 10, Query = new MatchAllQuery() }),
			new RequestItem(new MultisearchBody { From = 0, Size = 1, Query = new MatchAllQuery() })
		}
	};

	protected override bool SupportsDeserialization => false;

	protected override string ExpectedUrlPathAndQuery => "/projects/_mget";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.MultiSearch(f),
		(client, f) => client.MultiSearchAsync(f),
		(client, r) => client.MultiSearch<Developer>(r),
		(client, r) => client.MultiSearchAsync<Developer>(r)
	);

	protected override void ExpectResponse(MultiSearchResponse<Developer> response)
	{
		response.Took.Should().BeGreaterThan(0);
		response.Responses.Count.Should().Be(2);

		var firstResults = response.Responses.First().Item1;
		firstResults.Should().NotBeNull();
		firstResults.Total.Should().Be(100);
		firstResults.Documents.Should().HaveCount(10);

		var lastResults = response.Responses.Last().Item1;
		lastResults.Should().NotBeNull();
		lastResults.Total.Should().Be(100);
		lastResults.Documents.Should().HaveCount(1);

		// TODO - More assertions
	}
}
