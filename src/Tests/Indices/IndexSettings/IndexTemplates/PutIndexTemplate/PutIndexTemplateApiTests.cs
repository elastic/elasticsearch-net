using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.IndexSettings.IndexTemplates.PutIndexTemplate
{
	[Collection(IntegrationContext.Indexing)]
	public class PutIndexTemplateApiTests : ApiTestBase<IIndicesOperationResponse, IPutIndexTemplateRequest, PutIndexTemplateDescriptor, PutIndexTemplateRequest>
	{
		public PutIndexTemplateApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutIndexTemplate(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutIndexTemplateAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutIndexTemplate(r),
			requestAsync: (client, r) => client.PutIndexTemplateAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_template/{CallIsolatedValue}";

		protected override PutIndexTemplateDescriptor NewDescriptor()=> new PutIndexTemplateDescriptor(CallIsolatedValue);

		protected override Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> Fluent => d => d
			.Order(1)
			.Template("nest-*")
			.Create(false)
			.Settings(p=>p.NumberOfShards(1));

		protected override PutIndexTemplateRequest Initializer => new PutIndexTemplateRequest(CallIsolatedValue)
		{
			Order = 1,
			Tem  
			
		};
	}
}
