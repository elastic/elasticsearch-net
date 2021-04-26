/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations
{
	public abstract class AggregationUsageTestBase<TCluster>
		: ApiIntegrationTestBase<TCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
		where TCluster : INestTestCluster, IEphemeralCluster<EphemeralClusterConfiguration>, new()
	{
		protected AggregationUsageTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected virtual Nest.Indices AgainstIndex { get; } = Index<Project>();

		protected abstract object AggregationJson { get; }

		protected override bool ExpectIsValid => true;

		protected sealed override object ExpectJson => QueryScopeJson == null
			? (object)new { size = 0, aggs = AggregationJson }
			: new { size = 0, aggs = AggregationJson, query = QueryScopeJson };

		protected override int ExpectStatusCode => 200;

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Index(AgainstIndex)
			.TypedKeys(TestClient.Configuration.Random.TypedKeys)
			.Query(q => QueryScope)
			.Aggregations(FluentAggs);

		protected abstract Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs { get; }
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Project> Initializer =>
			new(AgainstIndex)
			{
				Query = QueryScope,
				Size = 0,
				TypedKeys = TestClient.Configuration.Random.TypedKeys,
				Aggregations = InitializerAggs
			};

		protected abstract AggregationDictionary InitializerAggs { get; }

		protected virtual QueryContainer QueryScope { get; } = new TermQuery { Field = "type", Value = Project.TypeName };

		protected virtual object QueryScopeJson { get; } = new { term = new { type = new { value = Project.TypeName } } };
		protected override string UrlPath => $"/project/_search";

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] protected override Task HitsTheCorrectUrl() => base.HitsTheCorrectUrl();

		[U] protected override Task UsesCorrectHttpMethod() => base.UsesCorrectHttpMethod();

		[U] protected override void SerializesInitializer() => base.SerializesInitializer();

		[U] protected override void SerializesFluent() => base.SerializesFluent();

		[I] public override Task ReturnsExpectedStatusCode() => base.ReturnsExpectedResponse();

		[I] public override Task ReturnsExpectedIsValid() => base.ReturnsExpectedIsValid();

		[I] public override Task ReturnsExpectedResponse() => base.ReturnsExpectedResponse();

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Search(f),
			(client, f) => client.SearchAsync(f),
			(client, r) => client.Search<Project>(r),
			(client, r) => client.SearchAsync<Project>(r)
		);
	}

	public abstract class ProjectsOnlyAggregationUsageTestBase : AggregationUsageTestBase<ReadOnlyCluster>
	{
		protected ProjectsOnlyAggregationUsageTestBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Nest.Indices AgainstIndex => DefaultSeeder.ProjectsAliasFilter;
		protected override string UrlPath => $"/{DefaultSeeder.ProjectsAliasFilter}/_search";

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] protected override Task HitsTheCorrectUrl() => base.HitsTheCorrectUrl();

		[U] protected override Task UsesCorrectHttpMethod() => base.UsesCorrectHttpMethod();

		[U] protected override void SerializesInitializer() => base.SerializesInitializer();

		[U] protected override void SerializesFluent() => base.SerializesFluent();

		[I] public override Task ReturnsExpectedStatusCode() => base.ReturnsExpectedResponse();

		[I] public override Task ReturnsExpectedIsValid() => base.ReturnsExpectedIsValid();

		[I] public override Task ReturnsExpectedResponse() => base.ReturnsExpectedResponse();
	}
}
