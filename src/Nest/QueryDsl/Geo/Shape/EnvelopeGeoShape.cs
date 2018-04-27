using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IEnvelopeGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}

	public class EnvelopeGeoShape : GeoShapeBase, IEnvelopeGeoShape
	{
		internal EnvelopeGeoShape() : base("envelope") { }

		public EnvelopeGeoShape(IEnumerable<GeoCoordinate> coordinates) : this() =>
			this.Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}
}
