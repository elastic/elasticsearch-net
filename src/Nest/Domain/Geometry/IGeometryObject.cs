using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IGeometryObject<TCoordinates>
	{
		string Type { get; }
		TCoordinates Coordinates { get; set; }
		GeoShape ToGeoShape();
	}
}
