using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGeoPolygonFilter : IFilterBase
	{
		[JsonProperty("points")]
		IEnumerable<string> Points { get; set; }
	}

	public class GeoPolygonFilter : FilterBase, IGeoPolygonFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				var gf = ((IGeoPolygonFilter)this);
				return !gf.Points.HasAny() || gf.Points.All(p => p.IsNullOrEmpty());
			}

		}

		IEnumerable<string> IGeoPolygonFilter.Points { get; set; }

	}
}
