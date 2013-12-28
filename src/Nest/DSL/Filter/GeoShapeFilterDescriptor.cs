using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class GeoShapeFilterDescriptor : FilterBase
	{
		internal override bool IsConditionless
		{
			get
			{
				return this._Shape == null || !this._Shape.Coordinates.HasAny();
			}

		}

		[JsonProperty("shape")]
		internal GeoShapeVector _Shape { get; set; }


		public GeoShapeFilterDescriptor Type(string type)
		{
			if (this._Shape == null)
				this._Shape = new GeoShapeVector();
			this._Shape.Type = type;
			return this;
		}

		public GeoShapeFilterDescriptor Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (this._Shape == null)
				this._Shape = new GeoShapeVector();
			this._Shape.Coordinates = coordinates;
			return this;
		}

	}

}
