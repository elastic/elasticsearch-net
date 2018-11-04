using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

#pragma warning disable 618 // PutSearchTemplate is deleted in 6.x

namespace Tests.Search.SearchTemplate.PutSearchTemplate
{
	public class PutSearchTemplateApiTests
		: ApiTestBase<ReadOnlyCluster, IPutSearchTemplateResponse, IPutSearchTemplateRequest, PutSearchTemplateDescriptor, PutSearchTemplateRequest>
	{
		private readonly string _template = "\"template\" : { \"query\": { \"match_all : {} } }\"";

		public PutSearchTemplateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			template = _template
		};

		protected override Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> Fluent => p => p
			.Template(_template);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutSearchTemplateRequest Initializer => new PutSearchTemplateRequest(CallIsolatedValue)
		{
			Template = _template
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_search/template/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.PutSearchTemplate(CallIsolatedValue, f),
			(c, f) => c.PutSearchTemplateAsync(CallIsolatedValue, f),
			(c, r) => c.PutSearchTemplate(r),
			(c, r) => c.PutSearchTemplateAsync(r)
		);

		protected override PutSearchTemplateDescriptor NewDescriptor() => new PutSearchTemplateDescriptor(CallIsolatedValue);
	}
}
