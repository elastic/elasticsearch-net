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
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeMultiPolygonQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IMultiPolygonGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPolygonQuery : PlainQuery, IGeoShapeMultiPolygonQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public PropertyPathMarker Field { get; set; }
		public IMultiPolygonGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.GeoShape = this;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}
	}

	public class GeoShapeMultiPolygonQueryDescriptor<T> : IGeoShapeMultiPolygonQuery where T : class
	{
		private IGeoShapeMultiPolygonQuery Self { get { return this; }}
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Field.IsConditionless() || Self.Shape == null || !Self.Shape.Coordinates.HasAny();
			}
		}
		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		IMultiPolygonGeoShape IGeoShapeMultiPolygonQuery.Shape { get; set; }

		public GeoShapeMultiPolygonQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoShapeMultiPolygonQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public GeoShapeMultiPolygonQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public GeoShapeMultiPolygonQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new MultiPolygonGeoShape();
			Self.Shape.Coordinates = coordinates;
			return this;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			Self.Field = fieldName;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return Self.Field;
		}
	}
}
