using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IGeoPointFielddata : IFielddata
	{
		[DataMember(Name ="format")]
		GeoPointFielddataFormat? Format { get; set; }

		[DataMember(Name ="precision")]
		Distance Precision { get; set; }
	}

	public class GeoPointFielddata : FielddataBase, IGeoPointFielddata
	{
		public GeoPointFielddataFormat? Format { get; set; }
		public Distance Precision { get; set; }
	}

	public class GeoPointFielddataDescriptor
		: FielddataDescriptorBase<GeoPointFielddataDescriptor, IGeoPointFielddata>, IGeoPointFielddata
	{
		GeoPointFielddataFormat? IGeoPointFielddata.Format { get; set; }
		Distance IGeoPointFielddata.Precision { get; set; }

		public GeoPointFielddataDescriptor Format(GeoPointFielddataFormat? format) => Assign(a => a.Format = format);

		public GeoPointFielddataDescriptor Precision(Distance distance) => Assign(a => a.Precision = distance);
	}
}
