using Elasticsearch.Net_5_2_0;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexSettings.IndexTemplates.DeleteIndexTemplate
{
	public class DeleteIndexTemplateApiTests
		: ApiTestBase<WritableCluster, IDeleteIndexTemplateResponse, IDeleteIndexTemplateRequest, DeleteIndexTemplateDescriptor, DeleteIndexTemplateRequest>
	{
		public DeleteIndexTemplateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
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
