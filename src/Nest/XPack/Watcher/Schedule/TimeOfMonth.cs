using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<TimeOfMonth>))]
	public interface ITimeOfMonth
	{
		[JsonProperty("at")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<string>))]
		IEnumerable<string> At { get; set; }

		[JsonProperty("on")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<int>))]
		IEnumerable<int> On { get; set; }
	}

	public class TimeOfMonth : ITimeOfMonth
	{
		public TimeOfMonth() { }

		public TimeOfMonth(int on, string at)
		{
			On = new[] { on };
			At = new[] { at };
		}

		public IEnumerable<string> At { get; set; }

		public IEnumerable<int> On { get; set; }
	}

	public class TimeOfMonthDescriptor : DescriptorBase<TimeOfMonthDescriptor, ITimeOfMonth>, ITimeOfMonth
	{
		IEnumerable<string> ITimeOfMonth.At { get; set; }
		IEnumerable<int> ITimeOfMonth.On { get; set; }

		public TimeOfMonthDescriptor On(IEnumerable<int> dates) => Assign(dates, (a, v) => a.On = v);

		public TimeOfMonthDescriptor On(params int[] dates) => Assign(dates, (a, v) => a.On = v);

		public TimeOfMonthDescriptor At(IEnumerable<string> time) => Assign(time, (a, v) => a.At = v);

		public TimeOfMonthDescriptor At(params string[] time) => Assign(time, (a, v) => a.At = v);
	}
}
