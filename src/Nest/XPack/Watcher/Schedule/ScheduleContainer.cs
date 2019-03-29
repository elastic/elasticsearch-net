using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ScheduleContainer>))]
	public interface IScheduleContainer
	{
		[JsonProperty("cron")]
		CronExpression Cron { get; set; }

		[JsonProperty("daily")]
		IDailySchedule Daily { get; set; }

		[JsonProperty("hourly")]
		IHourlySchedule Hourly { get; set; }

		[JsonProperty("interval")]
		Interval Interval { get; set; }

		[JsonProperty("monthly")]
		IMonthlySchedule Monthly { get; set; }

		[JsonProperty("weekly")]
		IWeeklySchedule Weekly { get; set; }

		[JsonProperty("yearly")]
		IYearlySchedule Yearly { get; set; }
	}

	public class ScheduleContainer : TriggerBase, IScheduleContainer
	{
		public ScheduleContainer() { }

		public ScheduleContainer(ScheduleBase schedule)
		{
			schedule.ThrowIfNull(nameof(schedule));
			schedule.WrapInContainer(this);
		}

		public CronExpression Cron { get; set; }

		public IDailySchedule Daily { get; set; }
		public IHourlySchedule Hourly { get; set; }
		public Interval Interval { get; set; }
		public IMonthlySchedule Monthly { get; set; }
		public IWeeklySchedule Weekly { get; set; }
		public IYearlySchedule Yearly { get; set; }

		internal override void WrapInContainer(ITriggerContainer container) => container.Schedule = this;

		public static implicit operator ScheduleContainer(ScheduleBase scheduleBase) => scheduleBase == null
			? null
			: new ScheduleContainer(scheduleBase);
	}

	public class ScheduleDescriptor : DescriptorBase<ScheduleDescriptor, IScheduleContainer>, IScheduleContainer
	{
		CronExpression IScheduleContainer.Cron { get; set; }
		IDailySchedule IScheduleContainer.Daily { get; set; }
		IHourlySchedule IScheduleContainer.Hourly { get; set; }
		Interval IScheduleContainer.Interval { get; set; }
		IMonthlySchedule IScheduleContainer.Monthly { get; set; }
		IWeeklySchedule IScheduleContainer.Weekly { get; set; }
		IYearlySchedule IScheduleContainer.Yearly { get; set; }

		public ScheduleDescriptor Daily(Func<DailyScheduleDescriptor, IDailySchedule> selector) =>
			Assign(selector.Invoke(new DailyScheduleDescriptor()), (a, v) => a.Daily = v);

		public ScheduleDescriptor Hourly(Func<HourlyScheduleDescriptor, IHourlySchedule> selector) =>
			Assign(selector.Invoke(new HourlyScheduleDescriptor()), (a, v) => a.Hourly = v);

		public ScheduleDescriptor Monthly(Func<MonthlyScheduleDescriptor, IPromise<IMonthlySchedule>> selector) =>
			Assign(selector.Invoke(new MonthlyScheduleDescriptor())?.Value, (a, v) => a.Monthly = v);

		public ScheduleDescriptor Weekly(Func<WeeklyScheduleDescriptor, IPromise<IWeeklySchedule>> selector) =>
			Assign(selector.Invoke(new WeeklyScheduleDescriptor())?.Value, (a, v) => a.Weekly = v);

		public ScheduleDescriptor Yearly(Func<YearlyScheduleDescriptor, IPromise<IYearlySchedule>> selector) =>
			Assign(selector.Invoke(new YearlyScheduleDescriptor())?.Value, (a, v) => a.Yearly = v);

		public ScheduleDescriptor Cron(CronExpression cron) => Assign(cron, (a, v) => a.Cron = v);

		public ScheduleDescriptor Interval(Interval interval) => Assign(interval, (a, v) => a.Interval = v);
	}
}
