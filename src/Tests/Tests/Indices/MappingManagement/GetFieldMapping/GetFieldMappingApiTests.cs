using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.Indices.MappingManagement.GetFieldMapping
{
	public class GetFieldMappingApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IGetFieldMappingResponse, IGetFieldMappingRequest, GetFieldMappingDescriptor<Project>, GetFieldMappingRequest>
	{
		private static readonly Fields Fields = Fields<Project>(p => p.Name, p => p.LeadDeveloper.IpAddress);
		private static readonly Field NameField = Field<Project>(p => p.Name);

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
		protected override string UrlPath => $"/_mapping/field/name%2CleadDeveloper.ipAddress";

		protected override GetFieldMappingDescriptor<Project> NewDescriptor() => new GetFieldMappingDescriptor<Project>(Fields);

		protected override GetFieldMappingRequest Initializer => new GetFieldMappingRequest(Fields);

		protected override void ExpectResponse(IGetFieldMappingResponse response)
		{
			response.Indices.Should()
				.NotBeEmpty("expect indices on the response")
				.And.ContainKey(Index<Project>(), "expect project to return field mappings")
				.And.ContainKey(Index<ProjectPercolation>(), "expect project percolation to return field mappings");

			var projectMappings = response.Indices[Index<Project>()];
			projectMappings.Should().NotBeNull("project mapping value in the dictionary should not point to a null value");
			projectMappings.Mappings.Should()
				.NotBeEmpty("project has fields so should contain a type mapping")
				.And.ContainKey(Type<Project>(), "the inferred type for project should be found in the index mapping");

			var projectTypeMappings = projectMappings.Mappings[Type<Project>()];
			projectTypeMappings.Should().NotBeNull("project type mapping value should not point to a null value");

			projectTypeMappings.Should()
				.NotBeEmpty("project mappings should return fields")
				.And.HaveCount(2, "project mapping should return both fields")
				.And.ContainKey(NameField, "name is a field in the project index");

			var fieldTypeMapping = projectTypeMappings[NameField];
			fieldTypeMapping.Should().NotBeNull("name field mapping should exist");
			fieldTypeMapping.FullName.Should().NotBeNullOrEmpty();
			fieldTypeMapping.Mapping.Should()
				.NotBeEmpty("field type mapping should return a `mapping` with the field information")
				.And.HaveCount(1, "field type mappings only return information from a single field")
				.And.ContainKey(NameField);

			var fieldMapping = fieldTypeMapping.Mapping[NameField];
			AssertNameFieldMapping(fieldMapping);

			fieldMapping = response.MappingFor<Project>(NameField);
			AssertNameFieldMapping(fieldMapping);

			fieldMapping = response.MappingFor<Project>(p=>p.Name);
			AssertNameFieldMapping(fieldMapping);
		}

		private static void AssertNameFieldMapping(IFieldMapping fieldMapping)
		{
			fieldMapping.Should().NotBeNull("expected to find name on field type mapping for project");
			var nameKeyword = (fieldMapping as KeywordProperty);
			nameKeyword.Should().NotBeNull("the field type is a keyword mapping");
			nameKeyword.Store.Should().BeTrue("name is keyword field that has store enabled");
			nameKeyword.Fields.Should().NotBeEmpty().And.HaveCount(2);
			nameKeyword.Fields["standard"].Should().NotBeNull();
		}
	}
}
