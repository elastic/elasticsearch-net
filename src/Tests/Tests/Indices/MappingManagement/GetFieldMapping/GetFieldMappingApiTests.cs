using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.MappingManagement.GetFieldMapping
{
	public class GetFieldMappingApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IGetFieldMappingResponse, IGetFieldMappingRequest, GetFieldMappingDescriptor<Project>,
			GetFieldMappingRequest>
	{
		private static readonly Fields Fields = Infer.Fields<Project>(p => p.Name, p => p.Tags);

		public GetFieldMappingApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetFieldMappingRequest Initializer => new GetFieldMappingRequest(Fields);
		protected override string UrlPath => $"/_mapping/field/name%2Ctags";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetFieldMapping<Project>(Fields),
			(client, f) => client.GetFieldMappingAsync<Project>(Fields),
			(client, r) => client.GetFieldMapping(r),
			(client, r) => client.GetFieldMappingAsync(r)
		);

		protected override GetFieldMappingDescriptor<Project> NewDescriptor() => new GetFieldMappingDescriptor<Project>(Fields);

		protected override void ExpectResponse(IGetFieldMappingResponse response)
		{
			var fieldMapping = response.MappingFor<Project>("name");
			fieldMapping.Should().NotBeNull();
		}
	}
}
