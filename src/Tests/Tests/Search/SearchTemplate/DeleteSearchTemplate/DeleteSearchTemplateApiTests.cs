using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Search.SearchTemplate.DeleteSearchTemplate
{
	public class DeleteSearchTemplateApiTests
		: ApiTestBase<WritableCluster, IDeleteSearchTemplateResponse, IDeleteSearchTemplateRequest, DeleteSearchTemplateDescriptor,
			DeleteSearchTemplateRequest>
	{
		public DeleteSearchTemplateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> Fluent => null;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteSearchTemplateRequest Initializer => new DeleteSearchTemplateRequest(CallIsolatedValue);
		protected override string UrlPath => $"/_search/template/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.DeleteSearchTemplate(CallIsolatedValue, f),
			(c, f) => c.DeleteSearchTemplateAsync(CallIsolatedValue, f),
			(c, r) => c.DeleteSearchTemplate(r),
			(c, r) => c.DeleteSearchTemplateAsync(r)
		);

		protected override DeleteSearchTemplateDescriptor NewDescriptor() => new DeleteSearchTemplateDescriptor(CallIsolatedValue);
	}
}
