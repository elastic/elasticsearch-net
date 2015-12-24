using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataNonStringMapping : FieldDataMapping
	{
		[JsonProperty("format")]
		public FieldDataNonStringFormat? Format { get; set; }

		[JsonProperty("precision")]
		public GeoPrecision Precision { get; set; }
	}
}
