using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Search.SearchTemplate.GetSearchTemplate
{
	[Collection(IntegrationContext.ReadOnly)]
	public class GetSearchTemplateApiTests : ApiTestBase<IGetSearchTemplateResponse, IGetSearchTemplateRequest, GetSearchTemplateDescriptor, GetSearchTemplateRequest>
	{
		public GetSearchTemplateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.GetSearchTemplate(CallIsolatedValue, f),
			fluentAsync: (c, f) => c.GetSearchTemplateAsync(CallIsolatedValue, f),
			request: (c, r) => c.GetSearchTemplate(r),
			requestAsync: (c, r) => c.GetSearchTemplateAsync(r)
		);
		
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_search/template/{CallIsolatedValue}";

		protected override GetSearchTemplateDescriptor NewDescriptor() => new GetSearchTemplateDescriptor(CallIsolatedValue);

		protected override Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> Fluent => null;

		protected override GetSearchTemplateRequest Initializer => new GetSearchTemplateRequest(CallIsolatedValue);
	}
}
