using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(GeoShapeFormatter<IPointGeoShape>))]
	public interface IPointGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		GeoCoordinate Coordinates { get; set; }
	}

	[JsonFormatter(typeof(GeoShapeFormatter<PointGeoShape>))]
	public class PointGeoShape : GeoShapeBase, IPointGeoShape
	{
		internal PointGeoShape() : base("point") { }

		public PointGeoShape(GeoCoordinate coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public GeoCoordinate Coordinates { get; set; }
	}
}
