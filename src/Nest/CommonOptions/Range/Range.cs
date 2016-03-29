using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<Range>))]
	public interface IRange 
	{
		[JsonProperty(PropertyName = "from")]
		double? From { get; set; }

		[JsonProperty(PropertyName = "to")]
		double? To { get; set; }

		[JsonProperty(PropertyName = "key")]
		string Key { get; set; }
	}

	public class Range : IRange
	{
		public double? From { get; set; }
		public double? To { get; set; }
		public string Key { get; set; }
	}

	public class RangeDescriptor
		: DescriptorBase<RangeDescriptor, IRange>, IRange
	{
		double? IRange.From { get; set; }
		string IRange.Key { get; set; }
		double? IRange.To { get; set; }

		public RangeDescriptor Key(string key) => Assign(a => a.Key = key);
		public RangeDescriptor From(double from) => Assign(a => a.From = from);
		public RangeDescriptor To(double to) => Assign(a => a.To = to);
	}
}
