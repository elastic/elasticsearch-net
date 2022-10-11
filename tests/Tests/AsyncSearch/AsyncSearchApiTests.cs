// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.AsyncSearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using System;
using Elastic.Clients.Elasticsearch.Experimental;

namespace Tests.AsyncSearch;

public class AsyncSearchApiTests : CoordinatedIntegrationTestBase<ReadOnlyCluster>
{
	private const string SubmitStep = nameof(SubmitStep);
	private const string StatusStep = nameof(StatusStep);
	private const string GetStep = nameof(GetStep);
	private const string DeleteStep = nameof(DeleteStep);

	public AsyncSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
	{
		{
			SubmitStep, u =>
			u.Calls<AsyncSearchSubmitRequestDescriptor<Project>, AsyncSearchSubmitRequest<Project>, AsyncSearchSubmitResponse<Project>>(
				v => new AsyncSearchSubmitRequest<Project>
				{
					Query = new MatchAllQuery(),
					KeepOnCompletion = true,
					WaitForCompletionTimeout = Duration.MinusOne,
					Aggregations = new TermsAggregation("states")
					{
						Field = Infer.Field<Project>(p => p.State.Suffix("keyword")),
						MinDocCount = 2,
						Size = 5,
						ShardSize = 100,
						ExecutionHint = TermsAggregationExecutionHint.Map,
						Missing = "n/a",
						// TODO - Review terms agg and fix this
						//Include = new TermsInclude(new[] { StateOfBeing.Stable.ToString(), StateOfBeing.VeryActive.ToString() }),
						Order =new []
						{
							AggregateOrder.KeyAscending,
							AggregateOrder.CountDescending
						},
						Meta = new Dictionary<string, object> { { "foo", "bar" } }
					}
				},
				(v, d) => d
					.MatchAll()
					.KeepOnCompletion()
					.WaitForCompletionTimeout(-1)
					.Aggregations(a => a
						.Terms("states", st => st
							.Field(p => p.State.Suffix("keyword"))
							.MinDocCount(2)
							.Size(5)
							.ShardSize(100)
							.ExecutionHint(TermsAggregationExecutionHint.Map)
							.Missing("n/a")
							// TODO - Review terms agg and fix this
							//.Include(new[] { StateOfBeing.Stable.ToString(), StateOfBeing.VeryActive.ToString() })
							.Order(new []
							{
								AggregateOrder.KeyAscending,
								AggregateOrder.CountDescending
							})
							.Meta(m => m
								.Add("foo", "bar")
							)
						)
					),
				(v, c, f) => c.AsyncSearch.Submit(f),
				(v, c, f) => c.AsyncSearch.SubmitAsync(f),
				(v, c, r) => c.AsyncSearch.Submit<Project>(r),
				(v, c, r) => c.AsyncSearch.SubmitAsync<Project>(r),
				onResponse: (r, values) => values.ExtendedValue("id", r.Id)
			)
		},
		{
			StatusStep, u =>
			u.Calls<AsyncSearchStatusRequestDescriptor, AsyncSearchStatusRequest, AsyncSearchStatusResponse>(
				v => new AsyncSearchStatusRequest(v),
				(v, d) => d,
				(v, c, f) => c.AsyncSearch.Status(v, f),
				(v, c, f) => c.AsyncSearch.StatusAsync(v, f),
				(v, c, r) => c.AsyncSearch.Status(r),
				(v, c, r) => c.AsyncSearch.StatusAsync(r),
				uniqueValueSelector: values => values.ExtendedValue<string>("id")
			)
		},
		{
			GetStep, u =>
			u.Calls<GetAsyncSearchRequestDescriptor<Project>, GetAsyncSearchRequest, GetAsyncSearchResponse<Project>>(
				v => new GetAsyncSearchRequest(v),
				(v, d) => d,
				(v, c, f) => c.AsyncSearch.Get(v, f),
				(v, c, f) => c.AsyncSearch.GetAsync(v, f),
				(v, c, r) => c.AsyncSearch.Get<Project>(r),
				(v, c, r) => c.AsyncSearch.GetAsync<Project>(r),
				uniqueValueSelector: values => values.ExtendedValue<string>("id")
			)
		},
		{
			DeleteStep, u =>
			u.Calls<DeleteAsyncSearchRequestDescriptor, DeleteAsyncSearchRequest, DeleteAsyncSearchResponse>(
				v => new DeleteAsyncSearchRequest(v),
				(v, d) => d,
				(v, c, f) => c.AsyncSearch.Delete(v, f),
				(v, c, f) => c.AsyncSearch.DeleteAsync(v, f),
				(v, c, r) => c.AsyncSearch.Delete(r),
				(v, c, r) => c.AsyncSearch.DeleteAsync(r),
				uniqueValueSelector: values => values.ExtendedValue<string>("id")
			)
		},
	})
	{ }

	[I]
	public async Task AsyncSearchSubmitResponse() => await Assert<AsyncSearchSubmitResponse<Project>>(SubmitStep, r =>
	{
		r.ShouldBeValid();
		r.Id.Should().NotBeNullOrEmpty();
		r.Response.Should().NotBeNull();
		r.Response.Took.Should().BeGreaterOrEqualTo(0);
	});

	[I]
	public async Task AsyncSearchStatusResponse() => await Assert<AsyncSearchStatusResponse>(StatusStep, r =>
	{
		r.ShouldBeValid();
		r.StartTimeInMillis.Should().BeGreaterThan(DateTimeOffset.UtcNow.AddMinutes(-10).ToUnixTimeMilliseconds());
		r.StartTimeInMillis.Should().BeLessOrEqualTo(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
		r.ExpirationTimeInMillis.Should().BeGreaterOrEqualTo(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

		if (r.IsRunning)
			r.CompletionStatus.HasValue.Should().BeFalse();
		else
			r.CompletionStatus?.Should().Be(200);

		r.Shards.Total.Should().BeGreaterOrEqualTo(1);
	});

	[I]
	public async Task AsyncSearchGetResponse() => await Assert<GetAsyncSearchResponse<Project>>(GetStep, (s, r) =>
	{
		r.ShouldBeValid();
		r.Id.Should().NotBeNullOrEmpty();
		r.StartTimeInMillis.Should().BeGreaterThan(DateTimeOffset.UtcNow.AddMinutes(-10).ToUnixTimeMilliseconds());
		r.StartTimeInMillis.Should().BeLessOrEqualTo(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
		r.ExpirationTimeInMillis.Should().BeGreaterOrEqualTo(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
		r.Response.Should().NotBeNull();
		r.Response.Took.Should().BeGreaterOrEqualTo(0);
		r.Response.Hits.Should().HaveCount(10);
		var terms = r.Response.Aggregations.GetTerms("states");
		terms.Should().NotBeNull();
		var stringTerms = r.Response.Aggregations.GetStringTerms("states");
		stringTerms.Should().NotBeNull();
		terms.Should().BeEquivalentTo(stringTerms);
	});

	[I]
	public async Task AsyncSearchDeleteResponse() => await Assert<DeleteAsyncSearchResponse>(DeleteStep, r =>
	{
		r.ShouldBeValid();
		r.Acknowledged.Should().BeTrue();
	});
}
