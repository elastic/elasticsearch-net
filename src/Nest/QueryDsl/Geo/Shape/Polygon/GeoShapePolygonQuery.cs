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
	public interface IGeoShapePolygonQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IPolygonGeoShape Shape { get; set; }
	}

	public class GeoShapePolygonQuery : PlainQuery, IGeoShapePolygonQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public PropertyPathMarker Field { get; set; }
		public IPolygonGeoShape Shape { get; set; }

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

	public class GeoShapePolygonQueryDescriptor<T> : IGeoShapePolygonQuery where T : class
	{
		private IGeoShapePolygonQuery Self { get { return this; } }
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Field.IsConditionless() || Self.Shape == null || !Self.Shape.Coordinates.HasAny();
			}

		}
		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		IPolygonGeoShape IGeoShapePolygonQuery.Shape { get; set; }

		public GeoShapePolygonQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public GeoShapePolygonQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoShapePolygonQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public GeoShapePolygonQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<IEnumerable<double>>> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new PolygonGeoShape();
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
