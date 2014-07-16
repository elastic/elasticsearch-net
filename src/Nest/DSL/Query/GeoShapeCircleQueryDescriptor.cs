using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeQuery : IFieldNameQuery
	{
		PropertyPathMarker Field { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeCircleQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		ICircleGeoShape Shape { get; set; }
	}

	public class GeoShapeCircleQuery : PlainQuery, IGeoShapeCircleQuery
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

		public ICircleGeoShape Shape { get; set; }
	}

	public class GeoShapeCircleQueryDescriptor<T> : IGeoShapeCircleQuery where T : class
	{
		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		
		ICircleGeoShape IGeoShapeCircleQuery.Shape { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || ((IGeoShapeCircleQuery)this).Shape == null || !((IGeoShapeCircleQuery)this).Shape.Coordinates.HasAny();
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
		
		public GeoShapeCircleQueryDescriptor<T> OnField(string field)
		{
			((IGeoShapeQuery)this).Field = field;
			return this;
		}

		public GeoShapeCircleQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IGeoShapeQuery)this).Field = objectPath;
			return this;
		}

		public GeoShapeCircleQueryDescriptor<T> Coordinates(IEnumerable<double> coordinates)
		{
			if (((IGeoShapeCircleQuery)this).Shape == null)
				((IGeoShapeCircleQuery)this).Shape = new CircleGeoShape();
			((IGeoShapeCircleQuery)this).Shape.Coordinates = coordinates;
			return this;
		}

		public GeoShapeCircleQueryDescriptor<T> Radius(string radius)
		{
			if (((IGeoShapeCircleQuery)this).Shape == null)
				((IGeoShapeCircleQuery)this).Shape = new CircleGeoShape();
			((IGeoShapeCircleQuery)this).Shape.Radius = radius;
			return this;
		}
	}
}
