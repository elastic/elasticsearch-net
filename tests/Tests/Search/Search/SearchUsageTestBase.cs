﻿// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Clients.Elasticsearch.Infer;

namespace Tests.Search.Search
{
	public abstract class SearchUsageTestBase
		: ApiIntegrationTestBase<ReadOnlyCluster, SearchResponse<Project>, SearchRequestDescriptor<Project>, SearchRequest<Project>>
	{
		protected TermQuery ProjectFilter = new()
		{
			Field = Field<Project>(p => p.Type),
			Value = Project.TypeName
		};

		protected object ProjectFilterExpectedJson = new { term = new { type = new { value = Project.TypeName } } };

		protected SearchUsageTestBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string ExpectedUrlPathAndQuery => "/project/_search";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Search(f),
			(client, f) => client.SearchAsync(f),
			(client, r) => client.Search<Project>(r),
			(client, r) => client.SearchAsync<Project>(r)
		);
	}
}
