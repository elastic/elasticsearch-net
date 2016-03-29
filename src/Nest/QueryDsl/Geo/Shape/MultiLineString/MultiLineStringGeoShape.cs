using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiLineStringGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}

	public class MultiLineStringGeoShape : GeoShapeBase, IMultiLineStringGeoShape
	{
		public MultiLineStringGeoShape() : this(null) { }

		public MultiLineStringGeoShape(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) 
			: base("multilinestring") 
		{
			this.Coordinates = coordinates;
		}

		public IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}
}
