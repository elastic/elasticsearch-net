using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiPointGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}

	public class MultiPointGeoShape : GeoShapeBase, IMultiPointGeoShape
	{
		internal MultiPointGeoShape() : base("multipoint") { }

		public MultiPointGeoShape(IEnumerable<GeoCoordinate> coordinates) : this() =>
			this.Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}
}
