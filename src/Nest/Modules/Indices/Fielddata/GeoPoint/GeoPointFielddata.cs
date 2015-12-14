using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoPointFielddata : IFielddata
	{
		[JsonProperty("precision")]
		Distance Precision { get; set; }

		[JsonProperty("format")]
		GeoPointFielddataFormat? Format { get; set; }
	}

	public class GeoPointFielddata : FielddataBase, IGeoPointFielddata
	{
		public Distance Precision { get; set; }

		public GeoPointFielddataFormat? Format { get; set; }
	}

	public class GeoPointFielddataDescriptor
		: FielddataDescriptorBase<GeoPointFielddataDescriptor, IGeoPointFielddata>, IGeoPointFielddata
	{
		GeoPointFielddataFormat? IGeoPointFielddata.Format { get; set; }
		Distance IGeoPointFielddata.Precision { get; set; }

		public GeoPointFielddataDescriptor Format(GeoPointFielddataFormat format) => Assign(a => a.Format = format);

		public GeoPointFielddataDescriptor Precision(Distance distance) => Assign(a => a.Precision = distance);
	}
}
