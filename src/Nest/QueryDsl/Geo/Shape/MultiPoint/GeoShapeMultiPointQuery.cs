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

	public class GeoShapeMultiPointQuery : FieldNameQueryBase, IGeoShapeMultiPointQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IMultiPointGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeMultiPointQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}
	
	public class GeoShapeMultiPointQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoShapeMultiPointQueryDescriptor<T>, IGeoShapeMultiPointQuery, T>
		, IGeoShapeMultiPointQuery where T : class
	{
		bool IQuery.Conditionless => GeoShapeMultiPointQuery.IsConditionless(this);
		IMultiPointGeoShape IGeoShapeMultiPointQuery.Shape { get; set; }

		public GeoShapeMultiPointQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates) => Assign(a =>
		{
			a.Shape = a.Shape ?? new MultiPointGeoShape();
			a.Shape.Coordinates = coordinates;
		});
	}
}
