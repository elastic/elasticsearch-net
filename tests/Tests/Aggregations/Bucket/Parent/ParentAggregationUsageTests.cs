// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Parent
{
	/**
	 * A special single bucket aggregation that selects parent documents that have the specified type, as defined in a `join` field.
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-bucket-parent-aggregation.html[Parent Aggregation].
	 */
	[SkipVersion("<6.6.0", "Parent aggregation only available in Elasticsearch 6.6.0+")]
	public class ParentAggregationUsageTests : ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<CommitActivity>, ISearchRequest, SearchDescriptor<CommitActivity>, SearchRequest<CommitActivity>>
	{
		public ParentAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override bool ExpectIsValid => true;

		protected sealed override object ExpectJson => new
		{
			size = 0,
			aggs = new
			{
				name_of_parent_agg = new
				{
					parent = new { type = "commits" },
					aggs = new
					{
						average_commits = new
						{
							avg = new { field = "numberOfCommits" }
						},
						max_commits = new
						{
							max = new { field = "numberOfCommits" }
						},
						min_commits = new
						{
							min = new { field = "numberOfCommits" }
						}
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		// hide
		protected override Func<SearchDescriptor<CommitActivity>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Index(DefaultSeeder.CommitsAliasFilter)
			.TypedKeys(TestClient.Configuration.Random.TypedKeys)
			.Aggregations(FluentAggs);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		// hide
		protected override SearchRequest<CommitActivity> Initializer =>
			new SearchRequest<CommitActivity>(DefaultSeeder.CommitsAliasFilter)
			{
				Size = 0,
				TypedKeys = TestClient.Configuration.Random.TypedKeys,
				Aggregations = InitializerAggs
			};

		protected override string UrlPath => $"/commits-only/_search";

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
			(client, r) => client.Search<CommitActivity>(r),
			(client, r) => client.SearchAsync<CommitActivity>(r)
		);

		protected Func<AggregationContainerDescriptor<CommitActivity>, IAggregationContainer> FluentAggs => a => a
			.Parent<Project>("name_of_parent_agg", parent => parent // <1> sub-aggregations are on the type determined from the generic type parameter. In this example, the search is against `CommitActivity` type and `Project` is a parent of `CommitActivity`
				.Aggregations(parentAggs => parentAggs
					.Average("average_commits", avg => avg.Field(p => p.NumberOfCommits))
					.Max("max_commits", avg => avg.Field(p => p.NumberOfCommits))
					.Min("min_commits", avg => avg.Field(p => p.NumberOfCommits))
				)
			);

		protected AggregationDictionary InitializerAggs =>
			new ParentAggregation("name_of_parent_agg", typeof(CommitActivity)) // <1> `join` field is determined from the _child_ type. In this example, it is `CommitActivity`
			{
				Aggregations =
					new AverageAggregation("average_commits", Field<Project>(f => f.NumberOfCommits)) // <2> sub-aggregations are on the type determined from the `join` field. In this example, a `Project` is a parent of `CommitActivity`
					&& new MaxAggregation("max_commits", Field<Project>(f => f.NumberOfCommits))
					&& new MinAggregation("min_commits", Field<Project>(f => f.NumberOfCommits))
			};

		protected override void ExpectResponse(ISearchResponse<CommitActivity> response)
		{
			response.ShouldBeValid();

			var parentAgg = response.Aggregations.Parent("name_of_parent_agg");
			parentAgg.Should().NotBeNull();
			parentAgg.DocCount.Should().BeGreaterThan(0);
			parentAgg.Min("average_commits").Should().NotBeNull();
			parentAgg.Min("min_commits").Should().NotBeNull();
			parentAgg.Max("max_commits").Should().NotBeNull();
		}
	}
}
