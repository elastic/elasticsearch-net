using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using Elasticsearch.Net;

namespace Tests.Indices.IndexManagement
{
	//TODO NOT Readonly
	[Collection(IntegrationContext.ReadOnly)]
	public abstract class PutMapping 
		: ApiCallIntegrationBase<IIndicesResponse, IPutMappingRequest, PutMappingDescriptor<Project>, PutMappingRequest<Project>>
	{
		protected PutMapping(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		public override bool ExpectIsValid => true;

		public override int ExpectStatusCode => 200;

		public override string UrlPath => "/project/project/_mapping";

		public override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Map(f),
			fluentAsync: (client, f) => client.MapAsync(f),
			request: (client, r) => client.Map(r),
			requestAsync: (client, r) => client.MapAsync(r)
		);
	}
}
