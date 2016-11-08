using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ScheduleContainer>))]
	public interface IScheduleContainer
	{
		[JsonProperty("hourly")]
		IHourlySchedule Hourly { get; set; }

		[JsonProperty("daily")]
		IDailySchedule Daily { get; set; }

		[JsonProperty("weekly")]
		IWeeklySchedule Weekly { get; set; }

		[JsonProperty("monthly")]
		IMonthlySchedule Monthly { get; set; }

		[JsonProperty("yearly")]
		IYearlySchedule Yearly { get; set; }

		[JsonProperty("cron")]
		CronExpression Cron { get; set; }

		[JsonProperty("interval")]
		Interval Interval { get; set; }
	}

	public class ScheduleContainer : TriggerBase, IScheduleContainer
	{
		public ScheduleContainer() {}

		public ScheduleContainer(ScheduleBase schedule)
		{
			schedule.ThrowIfNull(nameof(schedule));
			schedule.WrapInContainer(this);
		}

		public IDailySchedule Daily { get; set; }
		public IMonthlySchedule Monthly { get; set; }
		public IHourlySchedule Hourly { get; set; }
		public IWeeklySchedule Weekly { get; set; }
		public IYearlySchedule Yearly { get; set; }
		public CronExpression Cron { get; set; }
		public Interval Interval { get; set; }

		internal override void WrapInContainer(ITriggerContainer container) => container.Schedule = this;

		public static implicit operator ScheduleContainer(ScheduleBase scheduleBase) => scheduleBase == null
			? null
			: new ScheduleContainer(scheduleBase);
	}

	public class ScheduleDescriptor :
		DescriptorBase<ScheduleDescriptor, IScheduleContainer>, IScheduleContainer
	{
		IDailySchedule IScheduleContainer.Daily { get; set; }
		IMonthlySchedule IScheduleContainer.Monthly { get; set; }
		IHourlySchedule IScheduleContainer.Hourly { get; set; }
		IWeeklySchedule IScheduleContainer.Weekly { get; set; }
		IYearlySchedule IScheduleContainer.Yearly { get; set; }
		CronExpression IScheduleContainer.Cron { get; set; }
		Interval IScheduleContainer.Interval { get; set; }

		public ScheduleDescriptor Daily(Func<DailyScheduleDescriptor, IDailySchedule> selector) =>
			Assign(a => a.Daily = selector.Invoke(new DailyScheduleDescriptor()));

		public ScheduleDescriptor Hourly(Func<HourlyScheduleDescriptor, IHourlySchedule> selector) =>
			Assign(a => a.Hourly = selector.Invoke(new HourlyScheduleDescriptor()));

		public ScheduleDescriptor Monthly(Func<MonthlyScheduleDescriptor, IPromise<IMonthlySchedule>> selector) =>
			Assign(a => a.Monthly = selector.Invoke(new MonthlyScheduleDescriptor())?.Value);

		public ScheduleDescriptor Weekly(Func<WeeklyScheduleDescriptor, IPromise<IWeeklySchedule>> selector) =>
			Assign(a => a.Weekly = selector.Invoke(new WeeklyScheduleDescriptor())?.Value);

		public ScheduleDescriptor Yearly(Func<YearlyScheduleDescriptor, IPromise<IYearlySchedule>> selector) =>
			Assign(a => a.Yearly = selector.Invoke(new YearlyScheduleDescriptor())?.Value);

		public ScheduleDescriptor Cron(CronExpression cron) => Assign(a => a.Cron = cron);

		public ScheduleDescriptor Interval(Interval interval) => Assign(a => a.Interval = interval);
	}
}
