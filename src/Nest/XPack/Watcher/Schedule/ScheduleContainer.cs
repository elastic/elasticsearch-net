using System;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(ScheduleContainer))]
	public interface IScheduleContainer
	{
		[DataMember(Name ="cron")]
		CronExpression Cron { get; set; }

		[DataMember(Name ="daily")]
		IDailySchedule Daily { get; set; }

		[DataMember(Name ="hourly")]
		IHourlySchedule Hourly { get; set; }

		[DataMember(Name ="interval")]
		Interval Interval { get; set; }

		[DataMember(Name ="monthly")]
		IMonthlySchedule Monthly { get; set; }

		[DataMember(Name ="weekly")]
		IWeeklySchedule Weekly { get; set; }

		[DataMember(Name ="yearly")]
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
