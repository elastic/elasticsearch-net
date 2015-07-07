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
	public interface IGeoShapeMultiPointQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IMultiPointGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPointQuery : FieldNameQuery, IGeoShapeMultiPointQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IMultiPointGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeMultiPointQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}
	
	public class GeoShapeMultiPointQueryDescriptor<T> : IGeoShapeMultiPointQuery where T : class
	{
		private IGeoShapeMultiPointQuery Self { get { return this; }}
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => GeoShapeMultiPointQuery.IsConditionless(this);
		PropertyPath IFieldNameQuery.Field { get; set; }
		IMultiPointGeoShape IGeoShapeMultiPointQuery.Shape { get; set; }

		public GeoShapeMultiPointQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public GeoShapeMultiPointQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoShapeMultiPointQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public GeoShapeMultiPointQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new MultiPointGeoShape();
			Self.Shape.Coordinates = coordinates;
			return this;
		}
	}
}
