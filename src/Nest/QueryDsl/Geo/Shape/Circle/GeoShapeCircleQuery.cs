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
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeCircleQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		ICircleGeoShape Shape { get; set; }
	}

	public class GeoShapeCircleQuery : FieldNameQuery, IGeoShapeCircleQuery
	{
		bool IQuery.Conditionless { get { return false; } }
		public ICircleGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoShape = this;
	}


	public class GeoShapeCircleQueryDescriptor<T> : IGeoShapeCircleQuery where T : class
	{
		private IGeoShapeCircleQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || Self.Shape == null || !Self.Shape.Coordinates.HasAny();
			}

		}
		PropertyPathMarker IFieldNameQuery.Field { get; set; }
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
	}
}
