using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<GeoIndexedShapeFilterDescriptor>, CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoIndexedShapeFilter : IGeoShapeBaseFilter, ICustomJson
	{

		[JsonProperty("indexed_shape")]
		GeoIndexedShapeVector IndexedShape { get; set; }
	}

	public class GeoIndexedShapeFilterDescriptor : FilterBase, IGeoIndexedShapeFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IGeoIndexedShapeFilter)this).IndexedShape == null || ((IGeoIndexedShapeFilter)this).IndexedShape.Id.IsNullOrEmpty();
			}

		}

		PropertyPathMarker IGeoShapeBaseFilter.Field { get; set; }
		GeoIndexedShapeVector IGeoIndexedShapeFilter.IndexedShape { get; set; }


		public GeoIndexedShapeFilterDescriptor Lookup<T>(string field, string id, string index = null, string type = null)
		{
			return _SetShape<T>(field, id, index, type);
		}

		private GeoIndexedShapeFilterDescriptor _SetShape<T>(PropertyPathMarker field, string id, string index, string type)
		{
			((IGeoIndexedShapeFilter)this).IndexedShape = new GeoIndexedShapeVector
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
		
		object ICustomJson.GetCustomJson()
		{
			var f = (IGeoIndexedShapeFilter)this;
			var shape = new { indexed_shape = f.IndexedShape };
			return this.FieldNameAsKeyFormat(f.Field, shape);
		}
	}

}
