using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Search.SearchTemplate.GetSearchTemplate
{
	public class GetSearchTemplateApiTests
		: ApiTestBase<ReadOnlyCluster, IGetSearchTemplateResponse, IGetSearchTemplateRequest, GetSearchTemplateDescriptor, GetSearchTemplateRequest>
	{
		public GetSearchTemplateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> Fluent => null;

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetSearchTemplateRequest Initializer => new GetSearchTemplateRequest(CallIsolatedValue);
		protected override string UrlPath => $"/_search/template/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.GetSearchTemplate(CallIsolatedValue, f),
			(c, f) => c.GetSearchTemplateAsync(CallIsolatedValue, f),
			(c, r) => c.GetSearchTemplate(r),
			(c, r) => c.GetSearchTemplateAsync(r)
		);

		protected override GetSearchTemplateDescriptor NewDescriptor() => new GetSearchTemplateDescriptor(CallIsolatedValue);
	}
}
