using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Elasticsearch.Net;
using Newtonsoft.Json.Converters;

namespace Nest
{

	[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<GeoBoundingBoxFilter>, CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoBoundingBoxFilter : IFilterBase, ICustomJson
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
	}
}
