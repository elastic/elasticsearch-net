using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IpRangeAggregationRange>))]
	public interface IIpRangeAggregationRange
	{
		[JsonProperty("from")]
		string From { get; set; }

		[JsonProperty("to")]
		string To { get; set; }

		[JsonProperty("mask")]
		string Mask { get; set; }
	}

	public class IpRangeAggregationRange : IIpRangeAggregationRange
	{
		public string From { get; set; }

		public string To { get; set; }

		public string Mask { get; set; }
	}

	public class IpRangeAggregationRangeDescriptor
		: DescriptorBase<IpRangeAggregationRangeDescriptor, IIpRangeAggregationRange>, IIpRangeAggregationRange
	{
		string IIpRangeAggregationRange.From { get; set; }
		string IIpRangeAggregationRange.Mask { get; set; }
		string IIpRangeAggregationRange.To { get; set; }

		public IpRangeAggregationRangeDescriptor From(string from) => Assign(a => a.From = from);
		public IpRangeAggregationRangeDescriptor To(string to) => Assign(a => a.To = to);
		public IpRangeAggregationRangeDescriptor Mask(string mask) => Assign(a => a.Mask = mask);
	}
}
