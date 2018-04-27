using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ILineStringGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}

	public class LineStringGeoShape : GeoShapeBase, ILineStringGeoShape
	{
		internal LineStringGeoShape() : base("linestring") { }

		public LineStringGeoShape(IEnumerable<GeoCoordinate> coordinates) : this() =>
			this.Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}
}
