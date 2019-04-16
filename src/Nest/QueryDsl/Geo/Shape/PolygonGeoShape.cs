using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(GeoShapeFormatter<IPolygonGeoShape>))]
	public interface IPolygonGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}

	[JsonFormatter(typeof(GeoShapeFormatter<PolygonGeoShape>))]
	public class PolygonGeoShape : GeoShapeBase, IPolygonGeoShape
	{
		internal PolygonGeoShape() : base("polygon") { }

		public PolygonGeoShape(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}
}
