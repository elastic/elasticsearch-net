using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexSettings.IndexTemplates.GetIndexTemplate
{
	[Collection(IntegrationContext.Indexing)]
	public class GetIndexTemplateApiTests : ApiTestBase<IGetIndexTemplateResponse, IGetIndexTemplateRequest, GetIndexTemplateDescriptor, GetIndexTemplateRequest>
	{
		public GetIndexTemplateApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetIndexTemplate(f),
			fluentAsync: (client, f) => client.GetIndexTemplateAsync(f),
			request: (client, r) => client.GetIndexTemplate(r),
			requestAsync: (client, r) => client.GetIndexTemplateAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_template/{CallIsolatedValue}";

		protected override Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> Fluent => d => d
			.Name(CallIsolatedValue);

		protected override GetIndexTemplateRequest Initializer => new GetIndexTemplateRequest(CallIsolatedValue);
	}
}
