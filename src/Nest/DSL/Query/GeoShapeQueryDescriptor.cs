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
	public interface IGeoShapeQuery
	{
		PropertyPathMarker _Field { get; set; }

		[JsonProperty("shape")]
		GeoShapeVector _Shape { get; set; }
	}

	public class GeoShapeQueryDescriptor<T> : IQuery, IGeoShapeQuery where T : class
	{
		PropertyPathMarker IGeoShapeQuery._Field { get; set; }
		
		[JsonProperty("shape")]
		GeoShapeVector IGeoShapeQuery._Shape { get; set; }
		
		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this)._Field.IsConditionless() || (((IGeoShapeQuery)this)._Shape == null || !((IGeoShapeQuery)this)._Shape.Coordinates.HasAny());
			}

		}
		public GeoShapeQueryDescriptor<T> OnField(string field)
		{
			((IGeoShapeQuery)this)._Field = field;
			return this;
		}
		public GeoShapeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IGeoShapeQuery)this)._Field = objectPath;
			return this;
		}
		

		public GeoShapeQueryDescriptor<T> Type(string type)
		{
			if (((IGeoShapeQuery)this)._Shape == null)
				((IGeoShapeQuery)this)._Shape = new GeoShapeVector();
			((IGeoShapeQuery)this)._Shape.Type = type;
			return this;
		}

		public GeoShapeQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (((IGeoShapeQuery)this)._Shape == null)
				((IGeoShapeQuery)this)._Shape = new GeoShapeVector();
			((IGeoShapeQuery)this)._Shape.Coordinates = coordinates;
			return this;
		}

	}

}
