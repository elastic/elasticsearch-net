using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IpRange>))]
	public interface IIpRange 
	{
		[JsonProperty(PropertyName = "from")]
		string From { get; set; }

		[JsonProperty(PropertyName = "to")]
		string To { get; set; }

		[JsonProperty(PropertyName = "mask")]
		string Mask { get; set; }
	}

	public class IpRange : IIpRange
	{
		public string From { get; set; }

		public string To { get; set; }

		public string Mask { get; set; }
	}

	public class IpRangeDescriptor
		: DescriptorBase<IpRangeDescriptor, IIpRange>, IIpRange
	{
		string IIpRange.From { get; set; }
		string IIpRange.Mask { get; set; }
		string IIpRange.To { get; set; }

		public IpRangeDescriptor From(string from) => Assign(a => a.From = from);
		public IpRangeDescriptor To(string to) => Assign(a => a.To = to);
		public IpRangeDescriptor Mask(string mask) => Assign(a => a.Mask = mask);
	}
}
