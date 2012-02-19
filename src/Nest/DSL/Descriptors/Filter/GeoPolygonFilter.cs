using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class GeoPolygonFilter : FilterBase
	{
		[JsonProperty("points")]
		public IEnumerable<string> Points { get; set; }

	}
}
