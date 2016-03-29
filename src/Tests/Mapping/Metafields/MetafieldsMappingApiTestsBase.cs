using System;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;
using Tests.Framework.MockData;
using Tests.Framework.Integration;
using System.Collections.Generic;
using Elasticsearch.Net;
using static Nest.Infer;
using Xunit;

namespace Tests.Mapping.Metafields
{
	[Collection(IntegrationContext.ReadOnly)]
	public abstract class MetafieldsMappingApiTestsBase : ApiTestBase<IPutMappingResponse, IPutMappingRequest, PutMappingDescriptor<Project>, PutMappingRequest<Project>>
	{
		public MetafieldsMappingApiTestsBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Map(f),
			fluentAsync: (client, f) => client.MapAsync(f),
			request: (client, r) => client.Map(r),
			requestAsync: (client, r) => client.MapAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/{CallIsolatedValue}/project/_mapping";

	}
}
