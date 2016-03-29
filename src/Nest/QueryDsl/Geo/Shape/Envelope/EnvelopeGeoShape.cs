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
		public EnvelopeGeoShape() : this(null) { }

		public EnvelopeGeoShape(IEnumerable<GeoCoordinate> coordinates) 
			: base("envelope") 
		{
			this.Coordinates = coordinates;
		}

		public IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}
}
