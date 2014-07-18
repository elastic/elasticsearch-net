using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.DSL.Query.Behaviour;
using Nest.Resolvers;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeMultiPolygonQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IMultiPolygonGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPolygonQuery : PlainQuery, IGeoShapeMultiPolygonQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.GeoShape = this;
		}

		bool IQuery.IsConditionless { get { return false; } }

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}

		public PropertyPathMarker Field { get; set; }

		public IMultiPolygonGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPolygonQueryDescriptor<T> : IGeoShapeMultiPolygonQuery where T : class
	{
		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		
		IMultiPolygonGeoShape IGeoShapeMultiPolygonQuery.Shape { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || ((IGeoShapeMultiPolygonQuery)this).Shape == null || !((IGeoShapeMultiPolygonQuery)this).Shape.Coordinates.HasAny();
			}

		}
		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			((IGeoShapeQuery)this).Field = fieldName;
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IGeoShapeQuery)this).Field;
		}
		
		public GeoShapeMultiPolygonQueryDescriptor<T> OnField(string field)
		{
			((IGeoShapeQuery)this).Field = field;
			return this;
		}
		public GeoShapeMultiPolygonQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IGeoShapeQuery)this).Field = objectPath;
			return this;
		}

		public GeoShapeMultiPolygonQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>> coordinates)
		{
			if (((IGeoShapeMultiPolygonQuery)this).Shape == null)
				((IGeoShapeMultiPolygonQuery)this).Shape = new MultiPolygonGeoShape();
			((IGeoShapeMultiPolygonQuery)this).Shape.Coordinates = coordinates;
			return this;
		}
	}
}
