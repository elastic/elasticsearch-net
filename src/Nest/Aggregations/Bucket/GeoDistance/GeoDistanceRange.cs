using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<GeoDistanceRange>))]
	public interface IGeoDistanceRange : INestSerializable
	{
		[JsonProperty(PropertyName = "from")]
		double? From { get; set; }

		[JsonProperty(PropertyName = "to")]
		double? To { get; set; }

		[JsonProperty(PropertyName = "key")]
		string Key { get; set; }
	}

	public class GeoDistanceRange : IGeoDistanceRange
	{
		public double? From { get; set; }
		public double? To { get; set; }
		public string Key { get; set; }
	}

	public class GeoDistanceRangeDescriptor
		: DescriptorBase<GeoDistanceRangeDescriptor, IGeoDistanceRange>, IGeoDistanceRange
	{
		double? IGeoDistanceRange.From { get; set; }
		string IGeoDistanceRange.Key { get; set; }
		double? IGeoDistanceRange.To { get; set; }

		public GeoDistanceRangeDescriptor Key(string key) => Assign(a => a.Key = key);
		public GeoDistanceRangeDescriptor From(double from) => Assign(a => a.From = from);
		public GeoDistanceRangeDescriptor To(double to) => Assign(a => a.To = to);
	}
}
