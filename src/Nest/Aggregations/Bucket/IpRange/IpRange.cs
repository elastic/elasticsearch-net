using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IpRange>))]
	public interface IIpRange
	{
		[JsonProperty("from")]
		string From { get; set; }

		[JsonProperty("mask")]
		string Mask { get; set; }

		[JsonProperty("to")]
		string To { get; set; }
	}

	public class IpRange : IIpRange
	{
		public string From { get; set; }

		public string Mask { get; set; }

		public string To { get; set; }
	}

	public class IpRangeDescriptor
		: DescriptorBase<IpRangeDescriptor, IIpRange>, IIpRange
	{
		string IIpRange.From { get; set; }
		string IIpRange.Mask { get; set; }
		string IIpRange.To { get; set; }

		public IpRangeDescriptor From(string from) => Assign(from, (a, v) => a.From = v);

		public IpRangeDescriptor To(string to) => Assign(to, (a, v) => a.To = v);

		public IpRangeDescriptor Mask(string mask) => Assign(mask, (a, v) => a.Mask = v);
	}
}
