using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(TimeOfWeek))]
	public interface ITimeOfWeek
	{
		[DataMember(Name ="at")]
		[JsonFormatter(typeof(ReadSingleOrEnumerableFormatter<string>))]
		IEnumerable<string> At { get; set; }

		[DataMember(Name ="on")]
		[JsonFormatter(typeof(ReadSingleOrEnumerableFormatter<Day>))]
		IEnumerable<Day> On { get; set; }
	}

	public class TimeOfWeek : ITimeOfWeek
	{
		public TimeOfWeek() { }

		public TimeOfWeek(Day on, string at)
		{
			On = new[] { on };
			At = new[] { at };
		}

		public IEnumerable<string> At { get; set; }

		public IEnumerable<Day> On { get; set; }
	}

	public class TimeOfWeekDescriptor : DescriptorBase<TimeOfWeekDescriptor, ITimeOfWeek>, ITimeOfWeek
	{
		IEnumerable<string> ITimeOfWeek.At { get; set; }
		IEnumerable<Day> ITimeOfWeek.On { get; set; }

		public TimeOfWeekDescriptor On(IEnumerable<Day> day) => Assign(a => a.On = day);

		public TimeOfWeekDescriptor On(params Day[] day) => Assign(a => a.On = day);

		public TimeOfWeekDescriptor At(IEnumerable<string> time) => Assign(a => a.At = time);

		public TimeOfWeekDescriptor At(params string[] time) => Assign(a => a.At = time);
	}
}
