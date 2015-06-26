using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public PropertyPathMarker Field { get; set; }
		public ICircleGeoShape Shape { get; set; }

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

	public class GeoShapeCircleQueryDescriptor<T> : IGeoShapeCircleQuery where T : class
	{
		private IGeoShapeCircleQuery Self { get { return this; } }
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || Self.Shape == null || !Self.Shape.Coordinates.HasAny();
			}

		}
		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		ICircleGeoShape IGeoShapeCircleQuery.Shape { get; set; }

		public GeoShapeCircleQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public GeoShapeCircleQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoShapeCircleQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public GeoShapeCircleQueryDescriptor<T> Coordinates(IEnumerable<double> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new CircleGeoShape();
			Self.Shape.Coordinates = coordinates;
			return this;
		}

		public GeoShapeCircleQueryDescriptor<T> Radius(string radius)
		{
			if (Self.Shape == null)
				Self.Shape = new CircleGeoShape();
			Self.Shape.Radius = radius;
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
