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
using static Nest.Static;

namespace Tests.Indices.MappingManagement.GetMapping
{
	[Collection(IntegrationContext.Indexing)]
	public class GetMappingApiTests : ApiIntegrationTestBase<IGetMappingResponse, IGetMappingRequest, GetMappingDescriptor<Project>, GetMappingRequest>
	{
		public GetMappingApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetMapping<Project>(f),
			fluentAsync: (client, f) => client.GetMappingAsync<Project>(f),
			request: (client, r) => client.GetMapping(r),
			requestAsync: (client, r) => client.GetMappingAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/project/_mapping/project?ignore_unavailable=true";

		protected override Func<GetMappingDescriptor<Project>, IGetMappingRequest> Fluent => d => d
			.IgnoreUnavailable();

		protected override GetMappingRequest Initializer => new GetMappingRequest(Index<Project>(), Type<Project>())
		{
			IgnoreUnavailable = true
		};

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
