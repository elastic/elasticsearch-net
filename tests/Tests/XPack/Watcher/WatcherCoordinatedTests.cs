// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using FluentAssertions;
using Tests.Core.Extensions;

namespace Tests.XPack.Watcher
{
	public class WatcherWeeklyScheduleoCoordinatedTests : CoordinatedIntegrationTestBase<WatcherCluster>
	{
		private const string CreateWeeklyScheduleWatcher = nameof(CreateWeeklyScheduleWatcher);
		private const string GetWeeklyScheduleWatcher = nameof(GetWeeklyScheduleWatcher);
		private const string DeleteWeeklyScheduleWatcher = nameof(DeleteWeeklyScheduleWatcher);

		public WatcherWeeklyScheduleoCoordinatedTests(WatcherCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{CreateWeeklyScheduleWatcher, u =>
				u.Calls<PutWatchDescriptor, PutWatchRequest, IPutWatchRequest, PutWatchResponse>(
					v => new PutWatchRequest(v){
						Trigger = new ScheduleContainer
						{
							Weekly = new WeeklySchedule
							{
								new TimeOfWeek(Day.Monday, "17:00"),
								new TimeOfWeek(Day.Tuesday, "17:00"),
								new TimeOfWeek(Day.Wednesday, "17:00"),
								new TimeOfWeek(Day.Thursday, "17:00"),
								new TimeOfWeek(Day.Friday, "17:00"),
								new TimeOfWeek(Day.Saturday, "17:00"),
								new TimeOfWeek(Day.Sunday, "17:00"),
							}
						}},
					(v, d) => d.Trigger(t => t.Schedule(s => s.Weekly(w => w
						.Add(tow => tow.On(Day.Monday).At("17:00"))
						.Add(tow => tow.On(Day.Tuesday).At("17:00"))
						.Add(tow => tow.On(Day.Wednesday).At("17:00"))
						.Add(tow => tow.On(Day.Thursday).At("17:00"))
						.Add(tow => tow.On(Day.Friday).At("17:00"))
						.Add(tow => tow.On(Day.Saturday).At("17:00"))
						.Add(tow => tow.On(Day.Sunday).At("17:00"))
					))),
					(v, c, f) => c.Watcher.Put(v, f),
					(v, c, f) => c.Watcher.PutAsync(v, f),
					(v, c, r) => c.Watcher.Put(r),
					(v, c, r) => c.Watcher.PutAsync(r)
				)
			},
			{GetWeeklyScheduleWatcher, u =>
				u.Calls<GetWatchDescriptor, GetWatchRequest, IGetWatchRequest, GetWatchResponse>(
					v => new GetWatchRequest(v),
					(v, d) => d,
					(v, c, f) => c.Watcher.Get(v, f),
					(v, c, f) => c.Watcher.GetAsync(v, f),
					(v, c, r) => c.Watcher.Get(r),
					(v, c, r) => c.Watcher.GetAsync(r)
				)
			},
			{DeleteWeeklyScheduleWatcher, u =>
				u.Calls<DeleteWatchDescriptor, DeleteWatchRequest, IDeleteWatchRequest, DeleteWatchResponse>(
					v => new DeleteWatchRequest(v),
					(v, d) => d,
					(v, c, f) => c.Watcher.Delete(v, f),
					(v, c, f) => c.Watcher.DeleteAsync(v, f),
					(v, c, r) => c.Watcher.Delete(r),
					(v, c, r) => c.Watcher.DeleteAsync(r)
				)
			}
		})
		{ }

		[I] public async Task GetWeeklyScheduleWatcherResponse() => await Assert<GetWatchResponse>(GetWeeklyScheduleWatcher, (v, r) =>
		{
			r.ShouldBeValid();
			r.Id.Should().Be(v);
			r.Found.Should().BeTrue();
			r.Watch.Trigger.Should().BeAssignableTo<ITriggerContainer>().Subject.Schedule.Weekly.Should().NotBeNullOrEmpty().And.HaveCount(7);
		});
	}
}
