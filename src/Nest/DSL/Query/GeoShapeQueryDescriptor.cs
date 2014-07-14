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
	public interface IGeoShapeQuery : IFieldNameQuery
	{
		PropertyPathMarker Field { get; set; }

		[JsonProperty("shape")]
		GeoShape Shape { get; set; }
	}

	public class GeoShapeQuery : PlainQuery, IGeoShapeQuery
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

		public GeoShape Shape { get; set; }
	}

	public class GeoShapeQueryDescriptor<T> : IGeoShapeQuery where T : class
	{
		IGeoShapeQuery Self { get { return this; } }

		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		
		GeoShape IGeoShapeQuery.Shape { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return this.Self.Field.IsConditionless() || this.Self.Shape == null;
			}

		}
		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Self.Field = fieldName;
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Self.Field;
		}
		
		public GeoShapeQueryDescriptor<T> OnField(string field)
		{
			this.Self.Field = field;
			return this;
		}
		public GeoShapeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this.Self.Field = objectPath;
			return this;
		}

		public GeoShapeQueryDescriptor<T> Shape<TCoordinates>(IGeometryObject<TCoordinates> shape)
		{
			shape.ThrowIfNull("shape");
			this.Self.Shape = shape.ToGeoShape();
			return this;
		}
	}
}
