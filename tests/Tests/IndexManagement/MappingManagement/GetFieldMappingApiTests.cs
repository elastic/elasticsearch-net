// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.IndexManagement.MappingManagement;

public class GetFieldMappingApiTests : ApiIntegrationTestBase<ReadOnlyCluster, GetFieldMappingResponse, GetFieldMappingRequestDescriptor<Project>, GetFieldMappingRequest>
{
	// TODO: Introduce percolation assertions once seeded

	private static readonly Fields Fields = Infer.Fields<Project>(p => p.Name, p => p.LeadDeveloper.IpAddress);
	private static readonly Field NameField = Infer.Field<Project>(p => p.Name);
	private static readonly Indices OnIndices = Infer.Index<Project>()/*.And<ProjectPercolation>()*/;

	public GetFieldMappingApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;
	protected override HttpMethod ExpectHttpMethod => HttpMethod.GET;
	protected override GetFieldMappingRequest Initializer => new(OnIndices, Fields);
	protected override Action<GetFieldMappingRequestDescriptor<Project>> Fluent => d => d.Indices(OnIndices);

	protected override string ExpectedUrlPathAndQuery => "/project/_mapping/field/name%2CleadDeveloper.ipAddress";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Indices.GetFieldMapping(Fields, f),
		(client, f) => client.Indices.GetFieldMappingAsync(Fields, f),
		(client, r) => client.Indices.GetFieldMapping(r),
		(client, r) => client.Indices.GetFieldMappingAsync(r)
	);

	protected override GetFieldMappingRequestDescriptor<Project> NewDescriptor() => new(Fields);

	protected override void ExpectResponse(GetFieldMappingResponse response)
	{
		response.FieldMappings.Should()
			.NotBeEmpty("expect indices on the response")
			.And.ContainKey(Infer.Index<Project>(), "expect project to return field mappings");
			//.And.ContainKey(Infer.Index<ProjectPercolation>(), "expect project percolation to return field mappings");

		var projectMappings = response.FieldMappings[Infer.Index<Project>()];
		projectMappings.Should().NotBeNull("project mapping value in the dictionary should not point to a null value");

		// TODO - Reintroduce this once GetFieldMappingResponse.FieldMappings uses ResolvableReadOnlyDictionaryConverter
		//projectMappings.Mappings.Should()
		//	.NotBeEmpty("project has fields so should contain a type mapping")
		//	.And.ContainKey(NameField, "project mappings should have 'name'");

		//var fieldTypeMapping = projectMappings.Mappings[NameField];
		//fieldTypeMapping.Should().NotBeNull("name field mapping should exist");
		//fieldTypeMapping.FullName.Should().NotBeNullOrEmpty();
		//fieldTypeMapping.Mapping.Should()
		//	.NotBeEmpty("field type mapping should return a `mapping` with the field information")
		//	.And.HaveCount(1, "field type mappings only return information from a single field")
		//	.And.ContainKey(NameField);

		//var fieldMapping = fieldTypeMapping.Mapping[NameField];
		//AssertNameFieldMapping(fieldMapping);

		//fieldMapping = response.MappingFor<Project>(NameField);
		//AssertNameFieldMapping(fieldMapping);

		//fieldMapping = response.MappingFor<Project>(p => p.Name);
		//AssertNameFieldMapping(fieldMapping);
	}

	private static void AssertNameFieldMapping(FieldMapping fieldMapping)
	{
		fieldMapping.Should().NotBeNull("expected to find name on field type mapping for project");

		//var nameKeyword = fieldMapping as KeywordProperty;
		//nameKeyword.Should().NotBeNull("the field type is a keyword mapping");
		//nameKeyword.Store.Should().BeTrue("name is keyword field that has store enabled");
		//nameKeyword.Fields.Should().NotBeEmpty().And.HaveCount(2);
		//nameKeyword.Fields["standard"].Should().NotBeNull();
	}
}
