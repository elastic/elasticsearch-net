using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Indices.MappingManagement.GetFieldMapping
{
	[Collection(IntegrationContext.Indexing)]
	public class GetFieldMappingApiTests 
		: ApiIntegrationTestBase<IGetFieldMappingResponse, IGetFieldMappingRequest, GetFieldMappingDescriptor<Project>, GetFieldMappingRequest>
	{
		private static readonly FieldNames Fields = Static.Fields<Project>(p => p.Name, p => p.Tags);

		public GetFieldMappingApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetFieldMapping<Project>(Fields),
			fluentAsync: (client, f) => client.GetFieldMappingAsync<Project>(Fields),
			request: (client, r) => client.GetFieldMapping(r),
			requestAsync: (client, r) => client.GetFieldMappingAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_mapping/field/name,tags";

		protected override GetFieldMappingDescriptor<Project> NewDescriptor() => new GetFieldMappingDescriptor<Project>(Fields);

		protected override GetFieldMappingRequest Initializer => new GetFieldMappingRequest(Fields);

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});

	}
}
