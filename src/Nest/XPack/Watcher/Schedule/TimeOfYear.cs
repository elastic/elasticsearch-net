using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TimeOfYear>))]
	public interface ITimeOfYear
	{
		[JsonProperty("int")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<Month>))]
		IEnumerable<Month> In { get; set; }

		[JsonProperty("on")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<int>))]
		IEnumerable<int> On { get; set; }

		[JsonProperty("at")]
		[JsonConverter(typeof(ReadSingleOrEnumerableJsonConverter<string>))]
		IEnumerable<string> At { get; set; }
	}

	public class TimeOfYear : ITimeOfYear
	{
		public IEnumerable<Month> In { get; set; }

		public IEnumerable<int> On { get; set; }

		public IEnumerable<string> At { get; set; }
	}

	public class TimeOfYearDescriptor : DescriptorBase<TimeOfYearDescriptor, ITimeOfYear>, ITimeOfYear
	{
		IEnumerable<Month> ITimeOfYear.In { get; set; }
		IEnumerable<int> ITimeOfYear.On { get; set; }
		IEnumerable<string> ITimeOfYear.At { get; set; }

		public TimeOfYearDescriptor In(IEnumerable<Month> @in) => Assign(a => a.In = @in);

		public TimeOfYearDescriptor In(params Month[] @in) => Assign(a => a.In = @in);

		public TimeOfYearDescriptor On(IEnumerable<int> on) => Assign(a => a.On = on);

		public TimeOfYearDescriptor On(params int[] on) => Assign(a => a.On = on);

		public TimeOfYearDescriptor At(IEnumerable<string> time) => Assign(a => a.At = time);

		public TimeOfYearDescriptor At(params string[] time) => Assign(a => a.At = time);
	}
}
