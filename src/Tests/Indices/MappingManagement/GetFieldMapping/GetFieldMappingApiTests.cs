using Elasticsearch.Net;
using FluentAssertions;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Indices.MappingManagement.GetFieldMapping
{
	public class GetFieldMappingApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IGetFieldMappingResponse, IGetFieldMappingRequest, GetFieldMappingDescriptor<Project>, GetFieldMappingRequest>
	{
		private static readonly Fields Fields = Infer.Fields<Project>(p => p.Name, p => p.Tags);

		public GetFieldMappingApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetFieldMapping<Project>(Fields),
			fluentAsync: (client, f) => client.GetFieldMappingAsync<Project>(Fields),
			request: (client, r) => client.GetFieldMapping(r),
			requestAsync: (client, r) => client.GetFieldMappingAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_mapping/field/name%2Ctags";

		protected override GetFieldMappingDescriptor<Project> NewDescriptor() => new GetFieldMappingDescriptor<Project>(Fields);

		protected override GetFieldMappingRequest Initializer => new GetFieldMappingRequest(Fields);

		protected override void ExpectResponse(IGetFieldMappingResponse response)
		{
			var fieldMapping = response.MappingFor<Project>("name");
			fieldMapping.Should().NotBeNull();
		}
	}
}
