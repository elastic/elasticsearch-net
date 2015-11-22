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

	public class GeoShapePolygonQuery : FieldNameQueryBase, IGeoShapePolygonQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IPolygonGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapePolygonQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapePolygonQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoShapePolygonQueryDescriptor<T>, IGeoShapePolygonQuery, T>
		, IGeoShapePolygonQuery where T : class
	{
		bool IQuery.Conditionless => GeoShapePolygonQuery.IsConditionless(this);
		IPolygonGeoShape IGeoShapePolygonQuery.Shape { get; set; }

		public GeoShapePolygonQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<IEnumerable<double>>> coordinates) =>
			Assign(a => a.Shape = new PolygonGeoShape { Coordinates = coordinates });
	}
}
