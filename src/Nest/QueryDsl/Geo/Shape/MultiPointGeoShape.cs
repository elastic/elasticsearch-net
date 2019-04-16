using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(GeoShapeFormatter<IMultiPointGeoShape>))]
	public interface IMultiPointGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}

	[JsonFormatter(typeof(GeoShapeFormatter<MultiPointGeoShape>))]
	public class MultiPointGeoShape : GeoShapeBase, IMultiPointGeoShape
	{
		internal MultiPointGeoShape() : base("multipoint") { }

		public MultiPointGeoShape(IEnumerable<GeoCoordinate> coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}
}
