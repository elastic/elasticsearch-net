using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using Elasticsearch.Net;
using Newtonsoft.Json.Converters;

namespace Nest
{

	[JsonConverter(typeof(CompositeJsonConverter<GeoBoundingFilterJsonReader, CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoBoundingBoxFilter : IFilterBase, ICustomJson, ICustomJsonReader<GeoBoundingBoxFilter>
	{
		PropertyPathMarker Field { get; set; }
		
		[JsonProperty("top_left")]
		string TopLeft { get; set; }

		[JsonProperty("bottom_right")]
		string BottomRight { get; set; }
		
		[JsonProperty("type")]
		GeoExecution? GeoExecution { get; set; }
	}

	public class GeoBoundingBoxFilter : FilterBase, IGeoBoundingBoxFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				var f = ((IGeoBoundingBoxFilter)this);
				return f.Field.IsConditionless() || f.TopLeft.IsNullOrEmpty() || f.BottomRight.IsNullOrEmpty();
			}
		}

		PropertyPathMarker IGeoBoundingBoxFilter.Field { get; set; }

		string IGeoBoundingBoxFilter.TopLeft { get; set; }

		string IGeoBoundingBoxFilter.BottomRight { get; set; }

		GeoExecution? IGeoBoundingBoxFilter.GeoExecution { get; set; }

		object ICustomJson.GetCustomJson()
		{
			var gbf = (IGeoBoundingBoxFilter)this;
			var location = new { top_left = gbf.TopLeft, bottom_right = gbf.BottomRight };
			var dict = this.FieldNameAsKeyFormat(gbf.Field, location, d=>d.Add("type", gbf.GeoExecution));
			return dict;
		}

		public GeoBoundingBoxFilter FromJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var filter = new GeoBoundingBoxFilter();
			var dict = base.ReadToDict(reader, serializer, filter);
			if (dict.Count == 0) return filter;
			var kv = dict.First();
			var f = filter as IGeoBoundingBoxFilter;
			f.Field = kv.Key;
			return filter;
		}
	}
}
