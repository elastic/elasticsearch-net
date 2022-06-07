using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.MultiGet;

public class MultiGetSimplifiedApiTests
	: ApiIntegrationTestBase<ReadOnlyCluster, MultiGetResponse<Developer>, MultiGetRequestDescriptor<Developer>, MultiGetRequest>
{
	private readonly IEnumerable<long> _ids = Developer.Developers.Select(d => d.Id).Take(10);

	public MultiGetSimplifiedApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;

	protected override bool VerifyJson => true;

	protected override int ExpectStatusCode => 200;

	protected override Action<MultiGetRequestDescriptor<Developer>> Fluent => d => d
		.Index(Infer.Index<Developer>())
		.Docs(_ids.Select<long, Action<OperationDescriptor>>(id => d => d.Id(id)).ToArray());

	protected override HttpMethod HttpMethod => HttpMethod.POST;

	protected override MultiGetRequest Initializer => new(Infer.Index<Developer>())
	{
		Docs = _ids.Select(id => new Operation { Id = id })
	};

	protected override bool SupportsDeserialization => false;

	protected override string ExpectedUrlPathAndQuery => "/devs/_mget";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.MultiGet(f),
		(client, f) => client.MultiGetAsync(f),
		(client, r) => client.MultiGet<Developer>(r),
		(client, r) => client.MultiGetAsync<Developer>(r)
	);

	protected override void ExpectResponse(MultiGetResponse<Developer> response)
	{
		response.Docs.Should().NotBeEmpty().And.HaveCount(10);

		foreach (var document in response.Docs)
		{
			var getResult = document.Item1;
			var error = document.Item2;

			getResult.Should().NotBeNull();
			getResult.Index.Should().NotBeNullOrWhiteSpace();
			getResult.Id.Should().NotBeNullOrWhiteSpace();
			getResult.Found.Should().BeTrue();

			error.Should().BeNull();
		}
	}
}
