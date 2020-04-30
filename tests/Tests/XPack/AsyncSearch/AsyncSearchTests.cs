using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
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
	public class AsyncSearchTests : CoordinatedIntegrationTestBase<ReadOnlyCluster>
	{
		private const string SubmitStep = nameof(SubmitStep);
		private const string GetStep = nameof(GetStep);
		private const string DeleteStep = nameof(DeleteStep);

		public AsyncSearchTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, testOnlyOne: false)
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

		[I] public async Task AsyncSearchSubmitResponse() => await Assert<AsyncSearchSubmitResponse<Project>>(SubmitStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Response.Should().NotBeNull();
		});

		[I] public async Task AsyncSearchGetResponse() => await Assert<AsyncSearchGetResponse<Project>>(GetStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Id.Should().NotBeNullOrEmpty();
			r.StartTime.Should().BeOnOrBefore(DateTimeOffset.Now);
			r.Response.Should().NotBeNull();
		});

		[I] public async Task AsyncSearchDeleteResponse() => await Assert<AsyncSearchDeleteResponse>(DeleteStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});
	}
}
