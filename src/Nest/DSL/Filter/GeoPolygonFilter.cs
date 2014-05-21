using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonConverter(typeof(CompositeJsonConverter<GeoPolygonFilterJsonReader, CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoPolygonFilter : IFilter, ICustomJson
	{
		PropertyPathMarker Field { get; set; }

		[JsonProperty("points")]
		IEnumerable<string> Points { get; set; }
	}

	public class GeoPolygonFilter : FilterBase, IGeoPolygonFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				var gf = ((IGeoPolygonFilter)this);
				return !gf.Points.HasAny() || gf.Points.All(p => p.IsNullOrEmpty());
			}

		}

		PropertyPathMarker IGeoPolygonFilter.Field { get; set; }
		IEnumerable<string> IGeoPolygonFilter.Points { get; set; }

		object ICustomJson.GetCustomJson()
		{
			var f = (IGeoPolygonFilter)this;
			var shape = new { points = f.Points };
			return this.FieldNameAsKeyFormat(f.Field, shape);
		}
	}
}
