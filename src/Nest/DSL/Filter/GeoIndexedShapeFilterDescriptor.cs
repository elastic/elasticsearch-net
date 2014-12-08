using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoIndexedShapeFilter : IGeoShapeBaseFilter
	{

		[JsonProperty("indexed_shape")]
		IndexedGeoShape IndexedShape { get; set; }
	}

	public class GeoIndexedShapeFilter : PlainFilter, IGeoIndexedShapeFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.GeoShape = this;
		}

		public PropertyPathMarker Field { get; set; }

		public IndexedGeoShape IndexedShape { get; set; }

		public GeoShapeRelation? Relation { get; set; }
	}

	public class GeoIndexedShapeFilterDescriptor : FilterBase, IGeoIndexedShapeFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return ((IGeoIndexedShapeFilter)this).IndexedShape == null || ((IGeoIndexedShapeFilter)this).IndexedShape.Id.IsNullOrEmpty();
			}

		}

		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		GeoShapeRelation? IGeoShapeBaseFilter.Relation { get; set; }
		IndexedGeoShape IGeoIndexedShapeFilter.IndexedShape { get; set; }

		public GeoIndexedShapeFilterDescriptor Lookup<T>(string field, string id, string index = null, string type = null)
		{
			return _SetShape<T>(field, id, index, type);
		}

		private GeoIndexedShapeFilterDescriptor _SetShape<T>(PropertyPathMarker field, string id, string index, string type)
		{
			((IGeoIndexedShapeFilter)this).IndexedShape = new IndexedGeoShape
			{
				Field = field,
				Id = id,
				Type = type ?? new TypeNameMarker {Type = typeof (T)},
				Index = index ?? new IndexNameMarker {Type = typeof (T)}
			};
			return this;
		}

		public GeoIndexedShapeFilterDescriptor Lookup<T>(Expression<Func<T, object>> field, string id, string index = null, string type = null)
		{
			return _SetShape<T>(field, id, index, type);
		}
		
		public GeoIndexedShapeFilterDescriptor Relation<T>(GeoShapeRelation relation)
		{
			((IGeoIndexedShapeFilter)this).Relation = relation;
			return this;
		}
	}

}
