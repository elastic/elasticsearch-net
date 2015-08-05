using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoPointFielddata : IFielddata
	{
		[JsonProperty("precision")]
		GeoPrecision Precision { get; set; }

		[JsonProperty("format")]
		GeoPointFielddataFormat? Format { get; set; }
	}

	public class GeoPointFielddata : FielddataBase, IGeoPointFielddata
	{
		public GeoPrecision Precision { get; set; }

		public GeoPointFielddataFormat? Format { get; set; }
	}

	public class GeoPointFielddataDescriptor
		: FielddataDescriptorBase<GeoPointFielddataDescriptor, IGeoPointFielddata>, IGeoPointFielddata
	{
		GeoPointFielddataFormat? IGeoPointFielddata.Format { get; set; }
		GeoPrecision IGeoPointFielddata.Precision { get; set; }

		public GeoPointFielddataDescriptor Format(GeoPointFielddataFormat format) => Assign(a => a.Format = format);

		public GeoPointFielddataDescriptor Precision(GeoPrecision precision) => Assign(a => a.Precision = precision);
	}
}
