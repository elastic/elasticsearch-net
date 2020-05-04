// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Search.FieldCapabilities
{
	public class FieldCapabilitiesApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, FieldCapabilitiesResponse, IFieldCapabilitiesRequest, FieldCapabilitiesDescriptor,
			FieldCapabilitiesRequest>
	{
		public FieldCapabilitiesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override int ExpectStatusCode => 200;

		protected override Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> Fluent => d => d
			.Fields(Fields<Project>(p => p.Name).And("*"));

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override FieldCapabilitiesRequest Initializer => new FieldCapabilitiesRequest(Index<Project>().And<Developer>())
		{
			Fields = Fields<Project>(p => p.Name).And("*"),
		};

		protected override string UrlPath => "/project%2Cdevs/_field_caps?fields=name%2C%2A";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.FieldCapabilities(Index<Project>().And<Developer>(), f),
			(c, f) => c.FieldCapabilitiesAsync(Index<Project>().And<Developer>(), f),
			(c, r) => c.FieldCapabilities(r),
			(c, r) => c.FieldCapabilitiesAsync(r)
		);

		protected override void ExpectResponse(FieldCapabilitiesResponse response)
		{

			var sourceField = response.Fields.First(kv => kv.Value.Source != null).Value.Source;
			sourceField.Aggregatable.Should().BeFalse();
			sourceField.Searchable.Should().BeFalse();

			response.Fields.Should().ContainKey("_index");
			var indexField = response.Fields["_index"].Index;
			indexField.Should().NotBeNull();

			indexField.Aggregatable.Should().BeTrue();
			indexField.Searchable.Should().BeTrue();

			response.Fields.Should().ContainKey("jobTitle.keyword");
			var jobTitleCapabilities = response.Fields["jobTitle.keyword"].Keyword;
			jobTitleCapabilities.Aggregatable.Should().BeTrue();
			jobTitleCapabilities.Searchable.Should().BeTrue();

			jobTitleCapabilities = response.Fields[Field<Developer>(p => p.JobTitle.Suffix("keyword"))].Keyword;
			jobTitleCapabilities.Aggregatable.Should().BeTrue();
			jobTitleCapabilities.Searchable.Should().BeTrue();
		}
	}
}
