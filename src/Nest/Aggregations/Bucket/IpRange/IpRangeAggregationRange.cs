using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(IpRangeAggregationRange))]
	public interface IIpRangeAggregationRange
	{
		[DataMember(Name ="from")]
		string From { get; set; }

		[DataMember(Name ="mask")]
		string Mask { get; set; }

		[DataMember(Name ="to")]
		string To { get; set; }
	}

	public class IpRangeAggregationRange : IIpRangeAggregationRange
	{
		public string From { get; set; }

		public string Mask { get; set; }

		public string To { get; set; }
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
