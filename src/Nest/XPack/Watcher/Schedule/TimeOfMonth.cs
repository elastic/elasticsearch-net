using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(TimeOfMonth))]
	public interface ITimeOfMonth
	{
		[DataMember(Name ="at")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<string>))]
		IEnumerable<string> At { get; set; }

		[DataMember(Name ="on")]
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

		public TimeOfMonthDescriptor On(IEnumerable<int> dates) => Assign(a => a.On = dates);

		public TimeOfMonthDescriptor On(params int[] dates) => Assign(a => a.On = dates);

		public TimeOfMonthDescriptor At(IEnumerable<string> time) => Assign(a => a.At = time);

		public TimeOfMonthDescriptor At(params string[] time) => Assign(a => a.At = time);
	}
}
