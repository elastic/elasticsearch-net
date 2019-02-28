using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(GeoShapeFormatter<IEnvelopeGeoShape>))]
	public interface IEnvelopeGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}

	[JsonFormatter(typeof(GeoShapeFormatter<EnvelopeGeoShape>))]
	public class EnvelopeGeoShape : GeoShapeBase, IEnvelopeGeoShape
	{
		internal EnvelopeGeoShape() : base("envelope") { }

		public EnvelopeGeoShape(IEnumerable<GeoCoordinate> coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}
}
