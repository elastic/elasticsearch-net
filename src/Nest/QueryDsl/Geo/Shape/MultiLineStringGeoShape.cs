using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IMultiLineStringGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}

	public class MultiLineStringGeoShape : GeoShapeBase, IMultiLineStringGeoShape
	{
		internal MultiLineStringGeoShape() : base("multilinestring") { }

		public MultiLineStringGeoShape(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}
}
