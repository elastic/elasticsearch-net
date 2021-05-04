// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.AsyncSearch
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0 ")]
	public class AsyncSearchApiTests : CoordinatedIntegrationTestBase<ReadOnlyCluster>
	{
		private const string SubmitStep = nameof(SubmitStep);
		private const string StatusStep = nameof(StatusStep);
		private const string GetStep = nameof(GetStep);
		private const string DeleteStep = nameof(DeleteStep);

		public AsyncSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, testOnlyOne: true)
		{
			{SubmitStep, u =>
				u.Calls<AsyncSearchSubmitDescriptor<Project>, AsyncSearchSubmitRequest<Project>, IAsyncSearchSubmitRequest, AsyncSearchSubmitResponse<Project>>(
					v => new AsyncSearchSubmitRequest<Project>
					{
						Query = new MatchAllQuery(),
						KeepOnCompletion = true,
						WaitForCompletionTimeout = Time.MinusOne,
						Aggregations = new TermsAggregation("states")
						{
							Field = Infer.Field<Project>(p => p.State.Suffix("keyword")),
							MinimumDocumentCount = 2,
							Size = 5,
							ShardSize = 100,
							ExecutionHint = TermsAggregationExecutionHint.Map,
							Missing = "n/a",
							Include = new TermsInclude(new[] { StateOfBeing.Stable.ToString(), StateOfBeing.VeryActive.ToString() }),
							Order = new List<TermsOrder> { TermsOrder.KeyAscending, TermsOrder.CountDescending },
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
						),
					(v, c, f) => c.AsyncSearch.Submit(f),
					(v, c, f) => c.AsyncSearch.SubmitAsync(f),
					(v, c, r) => c.AsyncSearch.Submit<Project>(r),
					(v, c, r) => c.AsyncSearch.SubmitAsync<Project>(r),
					onResponse: (r, values) => values.ExtendedValue("id", r.Id)
				)
			},
			{StatusStep, ">=7.11.0", u =>
				u.Calls<AsyncSearchStatusDescriptor, AsyncSearchStatusRequest, IAsyncSearchStatusRequest, AsyncSearchStatusResponse>(
					v => new AsyncSearchStatusRequest(v),
					(v, d) => d,
					(v, c, f) => c.AsyncSearch.Status(v, f),
					(v, c, f) => c.AsyncSearch.StatusAsync(v, f),
					(v, c, r) => c.AsyncSearch.Status(r),
					(v, c, r) => c.AsyncSearch.StatusAsync(r),
					uniqueValueSelector: values => values.ExtendedValue<string>("id")
				)
			},
			{GetStep, u =>
				u.Calls<AsyncSearchGetDescriptor, AsyncSearchGetRequest, IAsyncSearchGetRequest, AsyncSearchGetResponse<Project>>(
					v => new AsyncSearchGetRequest(v),
					(v, d) => d,
					(v, c, f) => c.AsyncSearch.Get<Project>(v, f),
					(v, c, f) => c.AsyncSearch.GetAsync<Project>(v, f),
					(v, c, r) => c.AsyncSearch.Get<Project>(r),
					(v, c, r) => c.AsyncSearch.GetAsync<Project>(r),
					uniqueValueSelector: values => values.ExtendedValue<string>("id")
				)
			},
			{DeleteStep, u =>
				u.Calls<AsyncSearchDeleteDescriptor, AsyncSearchDeleteRequest, IAsyncSearchDeleteRequest, AsyncSearchDeleteResponse>(
					v => new AsyncSearchDeleteRequest(v),
					(v, d) => d,
					(v, c, f) => c.AsyncSearch.Delete(v, f),
					(v, c, f) => c.AsyncSearch.DeleteAsync(v, f),
					(v, c, r) => c.AsyncSearch.Delete(r),
					(v, c, r) => c.AsyncSearch.DeleteAsync(r),
					uniqueValueSelector: values => values.ExtendedValue<string>("id")
				)
			},
		}) { }

		[I] public async Task AsyncSearchSubmitResponse() => await Assert<AsyncSearchSubmitResponse<Project>>(SubmitStep, r =>
		{
			r.ShouldBeValid();
			r.Response.Should().NotBeNull();
			r.Response.Took.Should().BeGreaterOrEqualTo(0);
		});
		
		[I] public async Task AsyncSearchStatusResponse() => await Assert<AsyncSearchStatusResponse>(StatusStep, r =>
		{
			r.ShouldBeValid();
			r.StartTime.Should().BeOnOrBefore(DateTimeOffset.Now);
			r.ExpirationTime.Should().BeOnOrAfter(DateTimeOffset.Now);

			if (r.IsRunning)
				r.CompletionStatus.HasValue.Should().BeFalse();
			else
				r.CompletionStatus?.Should().Be(200);

			r.Shards.Total.Should().BeGreaterOrEqualTo(1);
		});

		[I] public async Task AsyncSearchGetResponse() => await Assert<AsyncSearchGetResponse<Project>>(GetStep, r =>
		{
			r.ShouldBeValid();
			r.Id.Should().NotBeNullOrEmpty();
			r.StartTime.Should().BeOnOrBefore(DateTimeOffset.Now);
			r.ExpirationTime.Should().BeOnOrAfter(DateTimeOffset.Now);
			r.Response.Should().NotBeNull();
			r.Response.Took.Should().BeGreaterOrEqualTo(0);
			r.Response.Hits.Should().HaveCount(10);
			var terms = r.Response.Aggregations.Terms("states");
			terms.Should().NotBeNull();
		});

		[I] public async Task AsyncSearchDeleteResponse() => await Assert<AsyncSearchDeleteResponse>(DeleteStep, r =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});
	}
}
