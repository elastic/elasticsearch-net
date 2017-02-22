using System;
using Elasticsearch.Net;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexSettings.IndexTemplates.GetIndexTemplate
{
	public class GetIndexTemplateApiTests
		: ApiTestBase<WritableCluster, IGetIndexTemplateResponse, IGetIndexTemplateRequest, GetIndexTemplateDescriptor, GetIndexTemplateRequest>
	{
		public GetIndexTemplateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
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
