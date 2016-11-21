using System;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ScheduleJsonConverter<IMonthlySchedule, MonthlySchedule, ITimeOfMonth>))]
	public interface IMonthlySchedule : ISchedule, IEnumerable<ITimeOfMonth> {}

	public class MonthlySchedule : ScheduleBase, IMonthlySchedule
	{
		private List<ITimeOfMonth> _times;

		public MonthlySchedule(IEnumerable<ITimeOfMonth> times)
		{
			this._times = times?.ToList();
		}

		public MonthlySchedule(params ITimeOfMonth[] times)
		{
			this._times = times?.ToList();
		}

		public MonthlySchedule() {}

		public void Add(ITimeOfMonth time)
		{
			if (_times == null) _times = new List<ITimeOfMonth>();
			_times.Add(time);
		}

		public IEnumerator<ITimeOfMonth> GetEnumerator() =>
			_times?.GetEnumerator() ?? Enumerable.Empty<ITimeOfMonth>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		internal override void WrapInContainer(IScheduleContainer container) => container.Monthly = this;

		public static implicit operator MonthlySchedule(ITimeOfMonth[] timesOfMonth) =>
			new MonthlySchedule(timesOfMonth);
	}

	public class MonthlyScheduleDescriptor :
		DescriptorPromiseBase<MonthlyScheduleDescriptor, MonthlySchedule>
	{
		public MonthlyScheduleDescriptor Add(Func<TimeOfMonthDescriptor, ITimeOfMonth> selector) =>
			Assign(a => a.Add(selector.InvokeOrDefault(new TimeOfMonthDescriptor())));

		public MonthlyScheduleDescriptor() : base(new MonthlySchedule()) {}
	}
}
