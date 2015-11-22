using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoIndexedShapeQuery : IGeoShapeQuery
	{
		[JsonProperty("indexed_shape")]
		IIndexedGeoShape IndexedShape { get; set; }
	}

	public class GeoIndexedShapeQuery : FieldNameQueryBase, IGeoIndexedShapeQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IIndexedGeoShape IndexedShape { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoShape = this;

		internal static bool IsConditionless(IGeoIndexedShapeQuery q) => q.Field.IsConditionless() || q.IndexedShape == null;
	}

	public class GeoIndexedShapeQueryDescriptor<T> : FieldNameQueryDescriptorBase<GeoIndexedShapeQueryDescriptor<T>, IGeoIndexedShapeQuery, T>
		, IGeoIndexedShapeQuery where T : class
	{
		bool IQuery.Conditionless => GeoIndexedShapeQuery.IsConditionless(this);
		IIndexedGeoShape IGeoIndexedShapeQuery.IndexedShape { get; set; }

		public GeoIndexedShapeQueryDescriptor<T> IndexedShape(Func<IndexedGeoShapeDescriptor<T>, IIndexedGeoShape> selector) =>
			Assign(a => a.IndexedShape = selector?.Invoke(new IndexedGeoShapeDescriptor<T>()));
	}
}
