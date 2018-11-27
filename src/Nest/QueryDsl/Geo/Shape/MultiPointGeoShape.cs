using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IMultiPointGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}

	public class MultiPointGeoShape : GeoShapeBase, IMultiPointGeoShape
	{
		internal MultiPointGeoShape() : base("multipoint") { }

		public MultiPointGeoShape(IEnumerable<GeoCoordinate> coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}
}
