using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[JsonConverter(typeof(ScheduleJsonConverter<IWeeklySchedule, WeeklySchedule, ITimeOfWeek>))]
	public interface IWeeklySchedule : ISchedule, IEnumerable<ITimeOfWeek> { }

	public class WeeklySchedule : ScheduleBase, IWeeklySchedule
	{
		private List<ITimeOfWeek> _times;

		public WeeklySchedule(IEnumerable<ITimeOfWeek> times) => _times = times?.ToList();

		public WeeklySchedule(params ITimeOfWeek[] times) => _times = times?.ToList();

		public WeeklySchedule() { }

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<ITimeOfWeek> GetEnumerator() => _times?.GetEnumerator() ?? Enumerable.Empty<ITimeOfWeek>().GetEnumerator();

		public void Add(ITimeOfWeek time)
		{
			if (_times == null) _times = new List<ITimeOfWeek>();
			_times.Add(time);
		}

		internal override void WrapInContainer(IScheduleContainer container) => container.Weekly = this;

		public static implicit operator WeeklySchedule(ITimeOfWeek[] timesOfWeek) =>
			new WeeklySchedule(timesOfWeek);
	}

	public class WeeklyScheduleDescriptor : DescriptorPromiseBase<WeeklyScheduleDescriptor, WeeklySchedule>
	{
		public WeeklyScheduleDescriptor() : base(new WeeklySchedule()) { }

		public WeeklyScheduleDescriptor Add(Func<TimeOfWeekDescriptor, ITimeOfWeek> selector) =>
			Assign(a => a.Add(selector.InvokeOrDefault(new TimeOfWeekDescriptor())));
	}

	internal class ScheduleJsonConverter<TSchedule, TReadAsSchedule, TTime> : ReadSingleOrEnumerableJsonConverter<TTime>
		where TSchedule : class, IEnumerable<TTime>
		where TReadAsSchedule : class, TSchedule
	{
		public override bool CanWrite => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var schedule = value as TSchedule;
			if (schedule == null)
			{
				writer.WriteNull();
				return;
			}

			var times = schedule.ToList();
			if (times.Count == 1) serializer.Serialize(writer, times[0]);
			else serializer.Serialize(writer, times);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var times = (TTime[])base.ReadJson(reader, objectType, existingValue, serializer);
			var schedule = typeof(TReadAsSchedule).CreateInstance(times);
			return schedule;
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
