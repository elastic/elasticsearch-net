using System;
using Elasticsearch.Net;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexSettings.IndexTemplates.IndexTemplateExists
{
	public class IndexTemplateExistsApiTests
		: ApiTestBase<WritableCluster, IExistsResponse, IIndexTemplateExistsRequest, IndexTemplateExistsDescriptor, IndexTemplateExistsRequest>
	{
		public IndexTemplateExistsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.IndexTemplateExists(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.IndexTemplateExistsAsync(CallIsolatedValue, f),
			request: (client, r) => client.IndexTemplateExists(r),
			requestAsync: (client, r) => client.IndexTemplateExistsAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/_template/{CallIsolatedValue}";

		protected override IndexTemplateExistsDescriptor NewDescriptor()=> new IndexTemplateExistsDescriptor(CallIsolatedValue);

		protected override Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> Fluent => d => d;

		protected override IndexTemplateExistsRequest Initializer => new IndexTemplateExistsRequest(CallIsolatedValue);
	}
}
