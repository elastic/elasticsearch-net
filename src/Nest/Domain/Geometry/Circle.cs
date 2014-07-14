using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class Circle 
		: GeometryObject<IEnumerable<double>>
	{
		public Circle() : this(null) { }

		public Circle(IEnumerable<double> coordinates)
			: base("circle")
		{
			this.Coordinates = coordinates ?? new List<double>();
		}

		public string Radius { get; set; }

		public override GeoShape ToGeoShape()
		{
			var shape = base.ToGeoShape();
			shape.Radius = this.Radius;
			return shape;
		}
	}
}
