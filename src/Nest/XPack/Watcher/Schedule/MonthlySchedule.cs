using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ScheduleFormatter<IMonthlySchedule, MonthlySchedule, ITimeOfMonth>))]
	public interface IMonthlySchedule : ISchedule, IEnumerable<ITimeOfMonth> { }

	public class MonthlySchedule : ScheduleBase, IMonthlySchedule
	{
		private List<ITimeOfMonth> _times;

		public MonthlySchedule(IEnumerable<ITimeOfMonth> times) => _times = times?.ToList();

		public MonthlySchedule(params ITimeOfMonth[] times) => _times = times?.ToList();

		public MonthlySchedule() { }

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<ITimeOfMonth> GetEnumerator() =>
			_times?.GetEnumerator() ?? Enumerable.Empty<ITimeOfMonth>().GetEnumerator();

		public void Add(ITimeOfMonth time)
		{
			if (_times == null) _times = new List<ITimeOfMonth>();
			_times.Add(time);
		}

		internal override void WrapInContainer(IScheduleContainer container) => container.Monthly = this;

		public static implicit operator MonthlySchedule(ITimeOfMonth[] timesOfMonth) =>
			new MonthlySchedule(timesOfMonth);
	}

	public class MonthlyScheduleDescriptor : DescriptorPromiseBase<MonthlyScheduleDescriptor, MonthlySchedule>
	{
		public MonthlyScheduleDescriptor() : base(new MonthlySchedule()) { }

		public MonthlyScheduleDescriptor Add(Func<TimeOfMonthDescriptor, ITimeOfMonth> selector) =>
			Assign(a => a.Add(selector.InvokeOrDefault(new TimeOfMonthDescriptor())));
	}
}
