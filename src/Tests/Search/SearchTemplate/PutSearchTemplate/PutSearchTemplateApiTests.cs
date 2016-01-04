using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Search.SearchTemplate.PutSearchTemplate
{
	[Collection(IntegrationContext.ReadOnly)]
	public class PutSearchTemplateApiTests : ApiTestBase<IPutSearchTemplateResponse, IPutSearchTemplateRequest, PutSearchTemplateDescriptor, PutSearchTemplateRequest>
	{
		public PutSearchTemplateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.PutSearchTemplate(CallIsolatedValue, f),
			fluentAsync: (c, f) => c.PutSearchTemplateAsync(CallIsolatedValue, f),
			request: (c, r) => c.PutSearchTemplate(r),
			requestAsync: (c, r) => c.PutSearchTemplateAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_search/template/{CallIsolatedValue}";

		private string _template = "\"template\" : { \"query\": { \"match_all : {} } }\"";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			template = _template
		};

		protected override PutSearchTemplateDescriptor NewDescriptor() => new PutSearchTemplateDescriptor(CallIsolatedValue);

		protected override Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> Fluent => p => p
			.Template(_template);

		protected override PutSearchTemplateRequest Initializer => new PutSearchTemplateRequest(CallIsolatedValue)
		{
			Template = _template
		};
	}
}
