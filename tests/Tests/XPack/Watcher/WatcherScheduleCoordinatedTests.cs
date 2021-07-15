// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Watcher
{
	public class WatcherScheduleCoordinatedTests : CoordinatedIntegrationTestBase<WatcherCluster>
	{
		private const string CreateCronExpressionScheduleWatcher = nameof(CreateCronExpressionScheduleWatcher);
		private const string CreateCronExpressionsScheduleWatcher = nameof(CreateCronExpressionsScheduleWatcher);
		private const string CreateDailyScheduleWatcher = nameof(CreateDailyScheduleWatcher);
		private const string CreateHourlyScheduleWatcher = nameof(CreateHourlyScheduleWatcher);
		private const string CreateMonthlyScheduleWatcher = nameof(CreateMonthlyScheduleWatcher);
		private const string CreateWeeklyScheduleWatcher = nameof(CreateWeeklyScheduleWatcher);
		private const string CreateYearlyScheduleWatcher = nameof(CreateYearlyScheduleWatcher);
		private const string DeleteCronExpressionScheduleWatcher = nameof(DeleteCronExpressionScheduleWatcher);
		private const string DeleteCronExpressionsScheduleWatcher = nameof(DeleteCronExpressionsScheduleWatcher);
		private const string DeleteDailyScheduleWatcher = nameof(DeleteDailyScheduleWatcher);
		private const string DeleteHourlyScheduleWatcher = nameof(DeleteHourlyScheduleWatcher);
		private const string DeleteMonthlyScheduleWatcher = nameof(DeleteMonthlyScheduleWatcher);
		private const string DeleteWeeklyScheduleWatcher = nameof(DeleteWeeklyScheduleWatcher);
		private const string DeleteYearlyScheduleWatcher = nameof(DeleteYearlyScheduleWatcher);
		private const string GetCronExpressionScheduleWatcher = nameof(GetCronExpressionScheduleWatcher);
		private const string GetCronExpressionsScheduleWatcher = nameof(GetCronExpressionsScheduleWatcher);
		private const string GetDailyScheduleWatcher = nameof(GetDailyScheduleWatcher);
		private const string GetHourlyScheduleWatcher = nameof(GetHourlyScheduleWatcher);
		private const string GetMonthlyScheduleWatcher = nameof(GetMonthlyScheduleWatcher);
		private const string GetWeeklyScheduleWatcher = nameof(GetWeeklyScheduleWatcher);
		private const string GetYearlyScheduleWatcher = nameof(GetYearlyScheduleWatcher);

		public WatcherScheduleCoordinatedTests(WatcherCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				CreateHourlyScheduleWatcher, u =>
					u.Calls<PutWatchDescriptor, PutWatchRequest, IPutWatchRequest, PutWatchResponse>(
						v => new PutWatchRequest($"hourly-{v}")
						{
							Trigger = new ScheduleContainer
							{
								Hourly = new HourlySchedule { Minute = new []{ 0, 30 }}
							}
						},
						(v, d) => d.Trigger(t => t.Schedule(s => s.Hourly(h => h.Minute(0, 30)))),
						(v, c, f) => c.Watcher.Put($"hourly-{v}", f),
						(v, c, f) => c.Watcher.PutAsync($"hourly-{v}", f),
						(v, c, r) => c.Watcher.Put(r),
						(v, c, r) => c.Watcher.PutAsync(r)
					)
			},
			{
				GetHourlyScheduleWatcher, u =>
					u.Calls<GetWatchDescriptor, GetWatchRequest, IGetWatchRequest, GetWatchResponse>(
						v => new GetWatchRequest($"hourly-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Get($"hourly-{v}", f),
						(v, c, f) => c.Watcher.GetAsync($"hourly-{v}", f),
						(v, c, r) => c.Watcher.Get(r),
						(v, c, r) => c.Watcher.GetAsync(r)
					)
			},
			{
				DeleteHourlyScheduleWatcher, u =>
					u.Calls<DeleteWatchDescriptor, DeleteWatchRequest, IDeleteWatchRequest, DeleteWatchResponse>(
						v => new DeleteWatchRequest($"hourly-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Delete($"hourly-{v}", f),
						(v, c, f) => c.Watcher.DeleteAsync($"hourly-{v}", f),
						(v, c, r) => c.Watcher.Delete(r),
						(v, c, r) => c.Watcher.DeleteAsync(r)
					)
			},
			{
				CreateDailyScheduleWatcher, u =>
					u.Calls<PutWatchDescriptor, PutWatchRequest, IPutWatchRequest, PutWatchResponse>(
						v => new PutWatchRequest($"daily-{v}")
						{
							Trigger = new ScheduleContainer
							{
								Daily = new DailySchedule(new TimeOfDay { Hour = new[] { 0, 6, 12, 18 }, Minute = new[] { 30 } })
							}
						},
						(v, d) => d.Trigger(t => t.Schedule(s => s.Daily(ds => ds.At(tod => tod.Hour(0, 6, 12, 18).Minute(30))))),
						(v, c, f) => c.Watcher.Put($"daily-{v}", f),
						(v, c, f) => c.Watcher.PutAsync($"daily-{v}", f),
						(v, c, r) => c.Watcher.Put(r),
						(v, c, r) => c.Watcher.PutAsync(r)
					)
			},
			{
				GetDailyScheduleWatcher, u =>
					u.Calls<GetWatchDescriptor, GetWatchRequest, IGetWatchRequest, GetWatchResponse>(
						v => new GetWatchRequest($"daily-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Get($"daily-{v}", f),
						(v, c, f) => c.Watcher.GetAsync($"daily-{v}", f),
						(v, c, r) => c.Watcher.Get(r),
						(v, c, r) => c.Watcher.GetAsync(r)
					)
			},
			{
				DeleteDailyScheduleWatcher, u =>
					u.Calls<DeleteWatchDescriptor, DeleteWatchRequest, IDeleteWatchRequest, DeleteWatchResponse>(
						v => new DeleteWatchRequest($"daily-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Delete($"daily-{v}", f),
						(v, c, f) => c.Watcher.DeleteAsync($"daily-{v}", f),
						(v, c, r) => c.Watcher.Delete(r),
						(v, c, r) => c.Watcher.DeleteAsync(r)
					)
			},
			{
				CreateWeeklyScheduleWatcher, u =>
					u.Calls<PutWatchDescriptor, PutWatchRequest, IPutWatchRequest, PutWatchResponse>(
						v => new PutWatchRequest($"weekly-{v}")
						{
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
							}
						},
						(v, d) => d.Trigger(t => t.Schedule(s => s.Weekly(w => w
							.Add(tow => tow.On(Day.Monday).At("17:00"))
							.Add(tow => tow.On(Day.Tuesday).At("17:00"))
							.Add(tow => tow.On(Day.Wednesday).At("17:00"))
							.Add(tow => tow.On(Day.Thursday).At("17:00"))
							.Add(tow => tow.On(Day.Friday).At("17:00"))
							.Add(tow => tow.On(Day.Saturday).At("17:00"))
							.Add(tow => tow.On(Day.Sunday).At("17:00"))
						))),
						(v, c, f) => c.Watcher.Put($"weekly-{v}", f),
						(v, c, f) => c.Watcher.PutAsync($"weekly-{v}", f),
						(v, c, r) => c.Watcher.Put(r),
						(v, c, r) => c.Watcher.PutAsync(r)
					)
			},
			{
				GetWeeklyScheduleWatcher, u =>
					u.Calls<GetWatchDescriptor, GetWatchRequest, IGetWatchRequest, GetWatchResponse>(
						v => new GetWatchRequest($"weekly-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Get($"weekly-{v}", f),
						(v, c, f) => c.Watcher.GetAsync($"weekly-{v}", f),
						(v, c, r) => c.Watcher.Get(r),
						(v, c, r) => c.Watcher.GetAsync(r)
					)
			},
			{
				DeleteWeeklyScheduleWatcher, u =>
					u.Calls<DeleteWatchDescriptor, DeleteWatchRequest, IDeleteWatchRequest, DeleteWatchResponse>(
						v => new DeleteWatchRequest($"weekly-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Delete($"weekly-{v}", f),
						(v, c, f) => c.Watcher.DeleteAsync($"weekly-{v}", f),
						(v, c, r) => c.Watcher.Delete(r),
						(v, c, r) => c.Watcher.DeleteAsync(r)
					)
			},
			{
				CreateMonthlyScheduleWatcher, u =>
					u.Calls<PutWatchDescriptor, PutWatchRequest, IPutWatchRequest, PutWatchResponse>(
						v => new PutWatchRequest($"monthly-{v}")
						{
							Trigger = new ScheduleContainer
							{
								Monthly = new MonthlySchedule(new TimeOfMonth(5, "09:00"), new TimeOfMonth(25, "17:00"))
							}
						},
						(v, d) => d.Trigger(t => t.Schedule(s => s.Monthly(m => m.Add(md => md.On(5).At("09:00")).Add(md => md.On(25).At("17:00"))))),
						(v, c, f) => c.Watcher.Put($"monthly-{v}", f),
						(v, c, f) => c.Watcher.PutAsync($"monthly-{v}", f),
						(v, c, r) => c.Watcher.Put(r),
						(v, c, r) => c.Watcher.PutAsync(r)
					)
			},
			{
				GetMonthlyScheduleWatcher, u =>
					u.Calls<GetWatchDescriptor, GetWatchRequest, IGetWatchRequest, GetWatchResponse>(
						v => new GetWatchRequest($"monthly-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Get($"monthly-{v}", f),
						(v, c, f) => c.Watcher.GetAsync($"monthly-{v}", f),
						(v, c, r) => c.Watcher.Get(r),
						(v, c, r) => c.Watcher.GetAsync(r)
					)
			},
			{
				DeleteMonthlyScheduleWatcher, u =>
					u.Calls<DeleteWatchDescriptor, DeleteWatchRequest, IDeleteWatchRequest, DeleteWatchResponse>(
						v => new DeleteWatchRequest($"monthly-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Delete($"monthly-{v}", f),
						(v, c, f) => c.Watcher.DeleteAsync($"monthly-{v}", f),
						(v, c, r) => c.Watcher.Delete(r),
						(v, c, r) => c.Watcher.DeleteAsync(r)
					)
			},
			{
				CreateYearlyScheduleWatcher, u =>
					u.Calls<PutWatchDescriptor, PutWatchRequest, IPutWatchRequest, PutWatchResponse>(
						v => new PutWatchRequest($"yearly-{v}")
						{
							Trigger = new ScheduleContainer
							{
								Yearly = new YearlySchedule
								{
									new TimeOfYear { In = new[] { Month.July }, On = new[] { 12 }, At = new[] { "08:00" } }
								}
							}
						},
						(v, d) => d.Trigger(t => t.Schedule(s => s.Yearly(y => y
							.Add(toy => toy.In(Month.July).On(12).At("08:00"))
						))),
						(v, c, f) => c.Watcher.Put($"yearly-{v}", f),
						(v, c, f) => c.Watcher.PutAsync($"yearly-{v}", f),
						(v, c, r) => c.Watcher.Put(r),
						(v, c, r) => c.Watcher.PutAsync(r)
					)
			},
			{
				GetYearlyScheduleWatcher, u =>
					u.Calls<GetWatchDescriptor, GetWatchRequest, IGetWatchRequest, GetWatchResponse>(
						v => new GetWatchRequest($"yearly-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Get($"yearly-{v}", f),
						(v, c, f) => c.Watcher.GetAsync($"yearly-{v}", f),
						(v, c, r) => c.Watcher.Get(r),
						(v, c, r) => c.Watcher.GetAsync(r)
					)
			},
			{
				DeleteYearlyScheduleWatcher, u =>
					u.Calls<DeleteWatchDescriptor, DeleteWatchRequest, IDeleteWatchRequest, DeleteWatchResponse>(
						v => new DeleteWatchRequest($"yearly-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Delete($"yearly-{v}", f),
						(v, c, f) => c.Watcher.DeleteAsync($"yearly-{v}", f),
						(v, c, r) => c.Watcher.Delete(r),
						(v, c, r) => c.Watcher.DeleteAsync(r)
					)
			},
			{
				CreateCronExpressionsScheduleWatcher, u =>
					u.Calls<PutWatchDescriptor, PutWatchRequest, IPutWatchRequest, PutWatchResponse>(
						v => new PutWatchRequest($"cronexpressions-{v}")
						{
							Trigger = new ScheduleContainer { CronExpressions = new CronExpressions { "0 0 12 * * ?", "0 0/2 * ? * MON-FRI" } }
						},
						(v, d) => d.Trigger(t => t.Schedule(s => s.CronExpressions(c => c.Add("0 0 12 * * ?").Add("0 0/2 * ? * MON-FRI")))),
						(v, c, f) => c.Watcher.Put($"cronexpressions-{v}", f),
						(v, c, f) => c.Watcher.PutAsync($"cronexpressions-{v}", f),
						(v, c, r) => c.Watcher.Put(r),
						(v, c, r) => c.Watcher.PutAsync(r)
					)
			},
			{
				GetCronExpressionsScheduleWatcher, u =>
					u.Calls<GetWatchDescriptor, GetWatchRequest, IGetWatchRequest, GetWatchResponse>(
						v => new GetWatchRequest($"cronexpressions-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Get($"cronexpressions-{v}", f),
						(v, c, f) => c.Watcher.GetAsync($"cronexpressions-{v}", f),
						(v, c, r) => c.Watcher.Get(r),
						(v, c, r) => c.Watcher.GetAsync(r)
					)
			},
			{
				DeleteCronExpressionsScheduleWatcher, u =>
					u.Calls<DeleteWatchDescriptor, DeleteWatchRequest, IDeleteWatchRequest, DeleteWatchResponse>(
						v => new DeleteWatchRequest($"cronexpressions-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Delete($"cronexpressions-{v}", f),
						(v, c, f) => c.Watcher.DeleteAsync($"cronexpressions-{v}", f),
						(v, c, r) => c.Watcher.Delete(r),
						(v, c, r) => c.Watcher.DeleteAsync(r)
					)
			},
			{
				CreateCronExpressionScheduleWatcher, u =>
					u.Calls<PutWatchDescriptor, PutWatchRequest, IPutWatchRequest, PutWatchResponse>(
						v => new PutWatchRequest($"cronexpression-{v}")
							{
								Trigger = new ScheduleContainer { Cron = new CronExpression("0 0 12 * * ?") }
							},
						(v, d) => d.Trigger(t => t.Schedule(s => s.Cron("0 0 12 * * ?"))),
						(v, c, f) => c.Watcher.Put($"cronexpression-{v}", f),
						(v, c, f) => c.Watcher.PutAsync($"cronexpression-{v}", f),
						(v, c, r) => c.Watcher.Put(r),
						(v, c, r) => c.Watcher.PutAsync(r)
					)
			},
			{
				GetCronExpressionScheduleWatcher, u =>
					u.Calls<GetWatchDescriptor, GetWatchRequest, IGetWatchRequest, GetWatchResponse>(
						v => new GetWatchRequest($"cronexpression-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Get($"cronexpression-{v}", f),
						(v, c, f) => c.Watcher.GetAsync($"cronexpression-{v}", f),
						(v, c, r) => c.Watcher.Get(r),
						(v, c, r) => c.Watcher.GetAsync(r)
					)
			},
			{
				DeleteCronExpressionScheduleWatcher, u =>
					u.Calls<DeleteWatchDescriptor, DeleteWatchRequest, IDeleteWatchRequest, DeleteWatchResponse>(
						v => new DeleteWatchRequest($"cronexpression-{v}"),
						(v, d) => d,
						(v, c, f) => c.Watcher.Delete($"cronexpression-{v}", f),
						(v, c, f) => c.Watcher.DeleteAsync($"cronexpression-{v}", f),
						(v, c, r) => c.Watcher.Delete(r),
						(v, c, r) => c.Watcher.DeleteAsync(r)
					)
			},
		}) { }

		[I] public async Task GetHourlyScheduleWatcherResponse() => await Assert<GetWatchResponse>(GetHourlyScheduleWatcher, (v, r) =>
		{
			r.ShouldBeValid();
			r.Id.Should().Be($"hourly-{v}");
			r.Found.Should().BeTrue();

			var container = r.Watch.Trigger.Should()
				.BeAssignableTo<ITriggerContainer>().Subject;
			
			container.Schedule.Hourly.Minute.Should().HaveCount(2);
			container.Schedule.Hourly.Minute.First().Should().Be(0);
			container.Schedule.Hourly.Minute.Last().Should().Be(30);
		});

		[I] public async Task GetDailyScheduleWatcherResponse() => await Assert<GetWatchResponse>(GetDailyScheduleWatcher, (v, r) =>
		{
			r.ShouldBeValid();
			r.Id.Should().Be($"daily-{v}");
			r.Found.Should().BeTrue();

			IEnumerable<ITimeOfDay> timeOfDay = null;
			r.Watch.Trigger.Should()
				.BeAssignableTo<ITriggerContainer>()
				.Subject.Schedule.Daily.At.Match(s => s.Should().BeNull(), tod => timeOfDay = tod);

			timeOfDay.Single().Hour.Should().ContainInOrder(0, 6, 12, 18);
			timeOfDay.Single().Minute.Should().ContainInOrder(30);
		});

		[I] public async Task GetWeeklyScheduleWatcherResponse() => await Assert<GetWatchResponse>(GetWeeklyScheduleWatcher, (v, r) =>
		{
			r.ShouldBeValid();
			r.Id.Should().Be($"weekly-{v}");
			r.Found.Should().BeTrue();
			r.Watch.Trigger.Should().BeAssignableTo<ITriggerContainer>().Subject.Schedule.Weekly.Should().HaveCount(7);
		});

		[I] public async Task GetMonthlyScheduleWatcherResponse() => await Assert<GetWatchResponse>(GetMonthlyScheduleWatcher, (v, r) =>
		{
			r.ShouldBeValid();
			r.Id.Should().Be($"monthly-{v}");
			r.Found.Should().BeTrue();
			r.Watch.Trigger.Should().BeAssignableTo<ITriggerContainer>().Subject.Schedule.Monthly.Should().HaveCount(2);

			var monthlySchedule = r.Watch.Trigger.As<ITriggerContainer>().Schedule.Monthly.First();
			monthlySchedule.On.First().Should().Be(5);
			monthlySchedule.At.First().Should().Be("09:00");

			monthlySchedule = r.Watch.Trigger.As<ITriggerContainer>().Schedule.Monthly.Last();
			monthlySchedule.On.First().Should().Be(25);
			monthlySchedule.At.First().Should().Be("17:00");
		});

		[I] public async Task GetYearlyScheduleWatcherResponse() => await Assert<GetWatchResponse>(GetYearlyScheduleWatcher, (v, r) =>
		{
			r.ShouldBeValid();
			r.Id.Should().Be($"yearly-{v}");
			r.Found.Should().BeTrue();
			r.Watch.Trigger.Should()
				.BeAssignableTo<ITriggerContainer>()
				.Subject.Schedule.Yearly.Should()
				.HaveCount(1);

			var yearlySchedule = r.Watch.Trigger.As<ITriggerContainer>().Schedule.Yearly.First();
			yearlySchedule.In.Should().HaveCount(1).And.Contain(Month.July);
			yearlySchedule.On.Should().HaveCount(1).And.Contain(12);
			yearlySchedule.At.Should().HaveCount(1).And.Contain("08:00");
		});

		[I] public async Task GetCronExpressionsScheduleWatcherResponse() => await Assert<GetWatchResponse>(GetCronExpressionsScheduleWatcher,
			(v, r) =>
			{
				r.ShouldBeValid();
				r.Id.Should().Be($"cronexpressions-{v}");
				r.Found.Should().BeTrue();
				r.Watch.Trigger.Should().BeAssignableTo<ITriggerContainer>().Subject.Schedule.CronExpressions.Should().HaveCount(2);
			});

		[I] public async Task GetCronExpresssionScheduleWatcherResponse() => await Assert<GetWatchResponse>(GetCronExpressionScheduleWatcher,
			(v, r) =>
			{
				r.ShouldBeValid();
				r.Id.Should().Be($"cronexpression-{v}");
				r.Found.Should().BeTrue();
				r.Watch.Trigger.Should().BeAssignableTo<ITriggerContainer>().Subject.Schedule.Cron.ToString().Should().Be("0 0 12 * * ?");
			});
	}
}
