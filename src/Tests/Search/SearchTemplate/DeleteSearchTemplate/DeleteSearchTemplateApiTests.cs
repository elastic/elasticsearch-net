using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Search.SearchTemplate.DeleteSearchTemplate
{
	[Collection(IntegrationContext.ReadOnly)]
	public class DeleteSearchTemplateApiTests : ApiTestBase<IDeleteSearchTemplateResponse, IDeleteSearchTemplateRequest, DeleteSearchTemplateDescriptor, DeleteSearchTemplateRequest>
	{
		public DeleteSearchTemplateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.DeleteSearchTemplate(CallIsolatedValue, f),
			fluentAsync: (c, f) => c.DeleteSearchTemplateAsync(CallIsolatedValue, f),
			request: (c, r) => c.DeleteSearchTemplate(r),
			requestAsync: (c, r) => c.DeleteSearchTemplateAsync(r)
		);
		
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_search/template/{CallIsolatedValue}";

		protected override DeleteSearchTemplateDescriptor NewDescriptor() => new DeleteSearchTemplateDescriptor(CallIsolatedValue);

		protected override Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> Fluent => null;

		protected override DeleteSearchTemplateRequest Initializer => new DeleteSearchTemplateRequest(CallIsolatedValue);
	}
}
