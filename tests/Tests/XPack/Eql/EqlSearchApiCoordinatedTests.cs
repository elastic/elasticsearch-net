// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Eql
{
	[SkipVersion("<7.11.0", "GA in 7.11.0")]
	public class EqlSearchApiCoordinatedTests : CoordinatedIntegrationTestBase<TimeSeriesCluster>
	{
		private const string SubmitStep = nameof(SubmitStep);
		private const string StatusStep = nameof(StatusStep);
		private const string GetStep = nameof(GetStep);
		private const string WaitStep = nameof(WaitStep);
		private const string DeleteStep = nameof(DeleteStep);

		public EqlSearchApiCoordinatedTests(TimeSeriesCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage, testOnlyOne: true)
		{
			{SubmitStep, u =>
				u.Calls<EqlSearchDescriptor<Log>, EqlSearchRequest<Log>, IEqlSearchRequest, EqlSearchResponse<Log>>(
					v => new EqlSearchRequest<Log>
					{
						Query = "any where true",
						KeepOnCompletion = true,
						TimestampField = Infer.Field<Log>(f => f.Timestamp),
						WaitForCompletionTimeout = "1nanos"
					},
					(v, d) => d
						.Query("any where true")
						.KeepOnCompletion()
						.TimestampField(Infer.Field<Log>(f => f.Timestamp))
						.WaitForCompletionTimeout("1nanos"),
					(v, c, f) => c.Eql.Search(f),
					(v, c, f) => c.Eql.SearchAsync(f),
					(v, c, r) => c.Eql.Search<Log>(r),
					(v, c, r) => c.Eql.SearchAsync<Log>(r),
					onResponse: (r, values) => values.ExtendedValue("id", r.Id)
				)
			},
			{StatusStep, u =>
				u.Calls<EqlSearchStatusDescriptor, EqlSearchStatusRequest, IEqlSearchStatusRequest, EqlSearchStatusResponse>(
					v => new EqlSearchStatusRequest(v),
					(v, d) => d,
					(v, c, f) => c.Eql.SearchStatus(v, f),
					(v, c, f) => c.Eql.SearchStatusAsync(v, f),
					(v, c, r) => c.Eql.SearchStatus(r),
					(v, c, r) => c.Eql.SearchStatusAsync(r),
					uniqueValueSelector: values => values.ExtendedValue<string>("id")
				)
			},
			{WaitStep, u => u.Call(async (v, c) =>
			{
				// wait for the search to complete
				var complete = false;
				var count = 0;

				while (!complete && count++ < 10)
				{
					await Task.Delay(100);
					var status = await c.Eql.SearchStatusAsync(u.Usage.CallUniqueValues.ExtendedValue<string>("id"));
					complete = !status.IsRunning;
				}
			})}, // allows the search to complete
			{GetStep, u =>
				u.Calls<EqlGetDescriptor, EqlGetRequest, IEqlGetRequest, EqlGetResponse<Log>>(
					v => new EqlGetRequest(v),
					(v, d) => d,
					(v, c, f) => c.Eql.Get<Log>(v, f),
					(v, c, f) => c.Eql.GetAsync<Log>(v, f),
					(v, c, r) => c.Eql.Get<Log>(r),
					(v, c, r) => c.Eql.GetAsync<Log>(r),
					uniqueValueSelector: values => values.ExtendedValue<string>("id")
				)
			},
			{DeleteStep, u =>
				u.Calls<EqlDeleteDescriptor, EqlDeleteRequest, IEqlDeleteRequest, EqlDeleteResponse>(
					v => new EqlDeleteRequest(v),
					(v, d) => d,
					(v, c, f) => c.Eql.Delete(v, f),
					(v, c, f) => c.Eql.DeleteAsync(v, f),
					(v, c, r) => c.Eql.Delete(r),
					(v, c, r) => c.Eql.DeleteAsync(r),
					uniqueValueSelector: values => values.ExtendedValue<string>("id")
				)
			}
		}) { }

		[I] public async Task EqlSearchResponse() => await Assert<EqlSearchResponse<Log>>(SubmitStep, r =>
		{
			r.ShouldBeValid();
			r.Id.Should().NotBeNullOrEmpty();
			r.IsPartial.Should().BeTrue();
			r.IsRunning.Should().BeTrue();
			r.TimedOut.Should().BeFalse();
		});
		
		[I] public async Task EqlSearchStatusResponse() => await Assert<EqlSearchStatusResponse>(StatusStep, r =>
		{
			r.ShouldBeValid();
			r.Id.Should().NotBeNullOrEmpty();
			r.IsPartial.Should().BeTrue();
			r.IsRunning.Should().BeTrue();
			r.ExpirationTimeInMillis.Should().BeGreaterThan(0);
			r.StartTimeInMillis.Should().BeGreaterThan(0);
		});

		[I] public async Task EqlGetResponse() => await Assert<EqlGetResponse<Log>>(GetStep, r =>
		{
			r.ShouldBeValid();
			r.IsPartial.Should().BeFalse();
			r.IsRunning.Should().BeFalse();
			r.Took.Should().BeGreaterOrEqualTo(0);
			r.TimedOut.Should().BeFalse();
			r.Events.Count.Should().Be(10);
			r.EqlHitsMetadata.Total.Value.Should().Be(10);
			r.Total.Should().Be(10);

			var firstEvent = r.Events.First();
			firstEvent.Index.Should().StartWith("customlogs-");
			firstEvent.Id.Should().NotBeNullOrEmpty();
			firstEvent.Source.Event.Category.Should().BeOneOf(Log.EventCategories);
		});

		[I] public async Task EqlDeleteResponse() => await Assert<EqlDeleteResponse>(DeleteStep, r =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});
	}
}