using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ScheduleJsonConverter<IYearlySchedule, YearlySchedule, ITimeOfYear>))]
	public interface IYearlySchedule : ISchedule, IEnumerable<ITimeOfYear> { }

	public class YearlySchedule : ScheduleBase, IYearlySchedule
	{
		private List<ITimeOfYear> _times;

		public YearlySchedule(IEnumerable<ITimeOfYear> times) => _times = times?.ToList();

		public YearlySchedule(params ITimeOfYear[] times) => _times = times?.ToList();

		public YearlySchedule() { }

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<ITimeOfYear> GetEnumerator() =>
			_times?.GetEnumerator() ?? Enumerable.Empty<ITimeOfYear>().GetEnumerator();

		public void Add(ITimeOfYear time)
		{
			if (_times == null) _times = new List<ITimeOfYear>();
			_times.Add(time);
		}

		public static implicit operator YearlySchedule(ITimeOfYear[] times) => new YearlySchedule(times);

		internal override void WrapInContainer(IScheduleContainer container) => container.Yearly = this;
	}

	public class YearlyScheduleDescriptor : DescriptorPromiseBase<YearlyScheduleDescriptor, YearlySchedule>
	{
		public YearlyScheduleDescriptor() : base(new YearlySchedule()) { }

		public YearlyScheduleDescriptor Add(Func<TimeOfYearDescriptor, ITimeOfYear> selector) =>
			Assign(a => a.Add(selector.InvokeOrDefault(new TimeOfYearDescriptor())));
	}
}
