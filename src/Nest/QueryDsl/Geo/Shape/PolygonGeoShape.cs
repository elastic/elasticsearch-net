using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPolygonGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}

	public class PolygonGeoShape : GeoShapeBase, IPolygonGeoShape
	{
		internal PolygonGeoShape() : base("polygon")  { }

		public PolygonGeoShape(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) : this() =>
			this.Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}
}
