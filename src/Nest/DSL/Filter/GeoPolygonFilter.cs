using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class GeoPolygonFilter : FilterBase
	{
		internal override bool IsConditionless
		{
			get
			{
				return !this.Points.HasAny() || this.Points.All(p=>p.IsNullOrEmpty());
			}

		}

		[JsonProperty("points")]
		public IEnumerable<string> Points { get; set; }

	}
}
