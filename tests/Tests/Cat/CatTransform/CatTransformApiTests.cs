using System;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatTransform
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class CatTransformApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatTransformRecord>, ICatTransformRequest, CatTransformDescriptor, CatTransformRequest>
	{
		public CatTransformApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/transforms";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Transform(f),
			(client, f) => client.Cat.TransformAsync(f),
			(client, r) => client.Cat.Transform(r),
			(client, r) => client.Cat.TransformAsync(r)
		);
	}

	public class CatTransformFullApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatTransformRecord>, ICatTransformRequest, CatTransformDescriptor,
			CatTransformRequest>
	{
		public CatTransformFullApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<CatTransformDescriptor, ICatTransformRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override CatTransformRequest Initializer { get; } = new CatTransformRequest { };

		protected override string UrlPath => "/_cat/transforms";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Transform(f),
			(client, f) => client.Cat.TransformAsync(f),
			(client, r) => client.Cat.Transform(r),
			(client, r) => client.Cat.TransformAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatTransformRecord> response) => response.ShouldBeValid();
	}
}
