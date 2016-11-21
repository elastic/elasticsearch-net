using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TimeOfMonth>))]
	public interface ITimeOfMonth
	{
		[JsonProperty("on")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<int>))]
		IEnumerable<int> On { get; set; }

		[JsonProperty("at")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<string>))]
		IEnumerable<string> At { get; set; }
	}

	public class TimeOfMonth : ITimeOfMonth
	{
		public TimeOfMonth()
		{
		}

		public TimeOfMonth(int on, string at)
		{
			On = new[] { on };
			At = new[] { at };
		}

		public IEnumerable<int> On { get; set; }

		public IEnumerable<string> At { get; set; }
	}

	public class TimeOfMonthDescriptor : DescriptorBase<TimeOfMonthDescriptor, ITimeOfMonth>, ITimeOfMonth
	{
		IEnumerable<int> ITimeOfMonth.On { get; set; }
		IEnumerable<string> ITimeOfMonth.At { get; set; }

		public TimeOfMonthDescriptor On(IEnumerable<int> dates) => Assign(a => a.On = dates);

		public TimeOfMonthDescriptor On(params int[] dates) => Assign(a => a.On = dates);

		public TimeOfMonthDescriptor At(IEnumerable<string> time) => Assign(a => a.At = time);

		public TimeOfMonthDescriptor At(params string[] time) => Assign(a => a.At = time);
	}
}
