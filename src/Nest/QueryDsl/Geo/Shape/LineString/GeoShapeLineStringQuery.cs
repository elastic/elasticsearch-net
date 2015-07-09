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
	public interface IGeoShapeLineStringQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		ILineStringGeoShape Shape { get; set; }
	}

	public class GeoShapeLineStringQuery : FieldNameQuery, IGeoShapeLineStringQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public ILineStringGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeLineStringQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeLineStringQueryDescriptor<T> 
		: FieldNameQueryDescriptor<GeoShapeLineStringQueryDescriptor<T>, IGeoShapeLineStringQuery, T>
		, IGeoShapeLineStringQuery where T : class
	{
		private IGeoShapeLineStringQuery Self => this;
		bool IQuery.Conditionless => GeoShapeLineStringQuery.IsConditionless(this);
		ILineStringGeoShape IGeoShapeLineStringQuery.Shape { get; set; }
		
		public GeoShapeLineStringQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new LineStringGeoShape();
			Self.Shape.Coordinates = coordinates;
			return this;
		}
	}
}
