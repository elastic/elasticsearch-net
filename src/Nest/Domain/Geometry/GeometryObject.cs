using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public abstract class GeometryObject<TCoordinates> 
		: IGeometryObject<TCoordinates>
	{
		public GeometryObject(string type)
		{
			this.Type = type;
		}

		public string Type { get; protected set; }

		public TCoordinates Coordinates { get; set; }

		public virtual GeoShape ToGeoShape()
		{
			var shape = new GeoShape
			{
				Type = this.Type,
				Coordinates = this.Coordinates
			};

			return shape;
		}
	}
}
