using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public class GeoShapeQueryDescriptor<T> : IQuery 
		where T : class
	{
		internal PropertyPathMarker _Field { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				return this._Field.IsConditionless() || (this._Shape == null || !this._Shape.Coordinates.HasAny());
			}

		}
		public GeoShapeQueryDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public GeoShapeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this._Field = objectPath;
			return this;
		}
		[JsonProperty("shape")]
		internal GeoShapeVector _Shape { get; set; }

		public GeoShapeQueryDescriptor<T> Type(string type)
		{
			if (this._Shape == null)
				this._Shape = new GeoShapeVector();
			this._Shape.Type = type;
			return this;
		}

		public GeoShapeQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (this._Shape == null)
				this._Shape = new GeoShapeVector();
			this._Shape.Coordinates = coordinates;
			return this;
		}

	}

}
