using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elasticsearch.Net.HttpMethod;
using static Nest.Infer;

namespace Tests.XPack.AsyncSearch.Submit
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class AsyncSearchSubmitApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, AsyncSearchSubmitResponse<Project>, IAsyncSearchSubmitRequest, AsyncSearchSubmitDescriptor<Project>, AsyncSearchSubmitRequest<Project>>
	{
		public AsyncSearchSubmitApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => POST;
		protected override string UrlPath => $"project/_async_search";
		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override object ExpectJson => new
		{
			query = new
			{
				match_all = new {}
			},
			aggs = new
			{
				states = new
				{
					meta = new
					{
						foo = "bar"
					},
					terms = new
					{
						field = "state.keyword",
						min_doc_count = 2,
						size = 5,
						shard_size = 100,
						execution_hint = "map",
						missing = "n/a",
						include = new[] { "Stable", "VeryActive" },
						order = new object[]
						{
							new { _key = "asc" },
							new { _count = "desc" }
						}
					}
				}
			}
		};

		protected override Func<AsyncSearchSubmitDescriptor<Project>, IAsyncSearchSubmitRequest> Fluent => f => f
			.MatchAll()
			.Aggregations(a => a
				.Terms("states", st => st
					.Field(p => p.State.Suffix("keyword"))
					.MinimumDocumentCount(2)
					.Size(5)
					.ShardSize(100)
					.ExecutionHint(TermsAggregationExecutionHint.Map)
					.Missing("n/a")
					.Include(new[] { StateOfBeing.Stable.ToString(), StateOfBeing.VeryActive.ToString() })
					.Order(o => o
						.KeyAscending()
						.CountDescending()
					)
					.Meta(m => m
						.Add("foo", "bar")
					)
				)
			);

		protected override AsyncSearchSubmitRequest<Project> Initializer => new AsyncSearchSubmitRequest<Project>
		{
			Query = new MatchAllQuery(),
			Aggregations = new TermsAggregation("states")
			{
				Field = Field<Project>(p => p.State.Suffix("keyword")),
				MinimumDocumentCount = 2,
				Size = 5,
				ShardSize = 100,
				ExecutionHint = TermsAggregationExecutionHint.Map,
				Missing = "n/a",
				Include = new TermsInclude(new[] { StateOfBeing.Stable.ToString(), StateOfBeing.VeryActive.ToString() }),
				Order = new List<TermsOrder> { TermsOrder.KeyAscending, TermsOrder.CountDescending },
				Meta = new Dictionary<string, object> { { "foo", "bar" } }
			}
		};

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.AsyncSearch.Submit(f),
			(client, f) => client.AsyncSearch.SubmitAsync(f),
			(client, r) => client.AsyncSearch.Submit<Project>(r),
			(client, r) => client.AsyncSearch.SubmitAsync<Project>(r)
		);

		protected override void ExpectResponse(AsyncSearchSubmitResponse<Project> response)
		{
			response.ShouldBeValid();
			response.Response.Should().NotBeNull();
		}
	}
}
