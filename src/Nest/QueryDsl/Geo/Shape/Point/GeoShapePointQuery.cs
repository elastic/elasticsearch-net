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
	public interface IGeoShapePointQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IPointGeoShape Shape { get; set; }
	}

	public class GeoShapePointQuery : FieldNameQuery, IGeoShapePointQuery
	{
		bool IQuery.Conditionless { get { return false; } }
		public IPointGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.GeoShape = this;
		}
	}

	public class GeoShapePointQueryDescriptor<T> : IGeoShapePointQuery where T : class
	{
		private IGeoShapePointQuery Self { get { return this; }}
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless
		{
			get
			{
				return Self.Field.IsConditionless() || Self.Shape == null || !Self.Shape.Coordinates.HasAny();
			}
		}
		PropertyPathMarker IFieldNameQuery.Field { get; set; }
		IPointGeoShape IGeoShapePointQuery.Shape { get; set; }
		
		public GeoShapePointQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public GeoShapePointQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoShapePointQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public GeoShapePointQueryDescriptor<T> Coordinates(IEnumerable<double> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new PointGeoShape();
			Self.Shape.Coordinates = coordinates;
			return this;
		}
	}
}
