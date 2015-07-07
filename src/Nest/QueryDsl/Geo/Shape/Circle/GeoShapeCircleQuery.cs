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
		bool IQuery.Conditionless => IsConditionless(this);
		public ICircleGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeCircleQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeCircleQueryDescriptor<T> : IGeoShapeCircleQuery where T : class
	{
		private IGeoShapeCircleQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => GeoShapeCircleQuery.IsConditionless(this);
		PropertyPath IFieldNameQuery.Field { get; set; }
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
