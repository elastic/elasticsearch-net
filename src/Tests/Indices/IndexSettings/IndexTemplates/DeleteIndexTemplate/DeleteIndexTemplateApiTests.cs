using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexSettings.IndexTemplates.DeleteIndexTemplate
{
	[Collection(IntegrationContext.Indexing)]
	public class DeleteIndexTemplateApiTests 
		: ApiTestBase<IDeleteIndexTemplateResponse, IDeleteIndexTemplateRequest, DeleteIndexTemplateDescriptor, DeleteIndexTemplateRequest>
	{
		public DeleteIndexTemplateApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteIndexTemplate(CallIsolatedValue),
			fluentAsync: (client, f) => client.DeleteIndexTemplateAsync(CallIsolatedValue),
			request: (client, r) => client.DeleteIndexTemplate(r),
			requestAsync: (client, r) => client.DeleteIndexTemplateAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_template/{CallIsolatedValue}";

		protected override DeleteIndexTemplateRequest Initializer => new DeleteIndexTemplateRequest(CallIsolatedValue);
	}
}
