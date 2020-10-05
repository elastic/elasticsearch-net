// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Indices.MappingManagement.GetFieldMapping
{
	public class GetFieldMappingApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, GetFieldMappingResponse, IGetFieldMappingRequest, GetFieldMappingDescriptor<Project>,
			GetFieldMappingRequest>
	{
		private static readonly Fields Fields = Fields<Project>(p => p.Name, p => p.LeadDeveloper.IpAddress);
		private static readonly Field NameField = Field<Project>(p => p.Name);
		private static readonly Nest.Indices OnIndices = Index<Project>().And<ProjectPercolation>();

		public GetFieldMappingApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetFieldMappingRequest Initializer => new GetFieldMappingRequest(OnIndices, Fields);
		protected override Func<GetFieldMappingDescriptor<Project>, IGetFieldMappingRequest> Fluent => d => d.Index(OnIndices);

		protected override string UrlPath => $"/project%2Cqueries/_mapping/field/name%2CleadDeveloper.ipAddress";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.GetFieldMapping(Fields, f),
			(client, f) => client.Indices.GetFieldMappingAsync(Fields, f),
			(client, r) => client.Indices.GetFieldMapping(r),
			(client, r) => client.Indices.GetFieldMappingAsync(r)
		);

		protected override GetFieldMappingDescriptor<Project> NewDescriptor() => new GetFieldMappingDescriptor<Project>(Fields);

		protected override void ExpectResponse(GetFieldMappingResponse response)
		{
			response.Indices.Should()
				.NotBeEmpty("expect indices on the response")
				.And.ContainKey(Index<Project>(), "expect project to return field mappings")
				.And.ContainKey(Index<ProjectPercolation>(), "expect project percolation to return field mappings");

			var projectMappings = response.Indices[Index<Project>()];
			projectMappings.Should().NotBeNull("project mapping value in the dictionary should not point to a null value");
			projectMappings.Mappings.Should()
				.NotBeEmpty("project has fields so should contain a type mapping")
				.And.ContainKey(NameField, "project mappings should have 'name'");

			var fieldTypeMapping = projectMappings.Mappings[NameField];
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

			fieldMapping = response.MappingFor<Project>(p => p.Name);
			AssertNameFieldMapping(fieldMapping);
		}

		private static void AssertNameFieldMapping(IFieldMapping fieldMapping)
		{
			fieldMapping.Should().NotBeNull("expected to find name on field type mapping for project");
			var nameKeyword = fieldMapping as KeywordProperty;
			nameKeyword.Should().NotBeNull("the field type is a keyword mapping");
			nameKeyword.Store.Should().BeTrue("name is keyword field that has store enabled");
			nameKeyword.Fields.Should().NotBeEmpty().And.HaveCount(2);
			nameKeyword.Fields["standard"].Should().NotBeNull();
		}
	}
}
