using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(GeoShapeFormatter<IMultiLineStringGeoShape>))]
	public interface IMultiLineStringGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}

	[JsonFormatter(typeof(GeoShapeFormatter<MultiLineStringGeoShape>))]
	public class MultiLineStringGeoShape : GeoShapeBase, IMultiLineStringGeoShape
	{
		internal MultiLineStringGeoShape() : base("multilinestring") { }

		public MultiLineStringGeoShape(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}
}
