using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexSettings.IndexTemplates.PutIndexTemplate
{
	[Collection(IntegrationContext.Indexing)]
	public class PutIndexTemplateApiTests : ApiTestBase<IPutIndexTemplateResponse, IPutIndexTemplateRequest, PutIndexTemplateDescriptor, PutIndexTemplateRequest>
	{
		public PutIndexTemplateApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutIndexTemplate(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutIndexTemplateAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutIndexTemplate(r),
			requestAsync: (client, r) => client.PutIndexTemplateAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_template/{CallIsolatedValue}?create=false";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			order = 1,
			template = "nestx-*",
			settings = new Dictionary<string, object> { { "index.number_of_shards", 1} }
		};

		protected override PutIndexTemplateDescriptor NewDescriptor() => new PutIndexTemplateDescriptor(CallIsolatedValue);

		protected override Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> Fluent => d => d
			.Order(1)
			.Template("nestx-*")
			.Create(false)
			.Settings(p=>p.NumberOfShards(1));

		protected override PutIndexTemplateRequest Initializer => new PutIndexTemplateRequest(CallIsolatedValue)
		{
			Order = 1,
			Template = "nestx-*",
			Create = false,
			Settings = new Nest.IndexSettings
			{
				NumberOfShards = 1
			}
		};
	}
}
