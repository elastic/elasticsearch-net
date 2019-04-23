using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<TimeOfWeek>))]
	public interface ITimeOfWeek
	{
		[JsonProperty("at")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<string>))]
		IEnumerable<string> At { get; set; }

		[JsonProperty("on")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<Day>))]
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

		public TimeOfWeekDescriptor On(IEnumerable<Day> day) => Assign(day, (a, v) => a.On = v);

		public TimeOfWeekDescriptor On(params Day[] day) => Assign(day, (a, v) => a.On = v);

		public TimeOfWeekDescriptor At(IEnumerable<string> time) => Assign(time, (a, v) => a.At = v);

		public TimeOfWeekDescriptor At(params string[] time) => Assign(time, (a, v) => a.At = v);
	}
}
