using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataStringMapping : FieldDataMapping
	{
		[JsonProperty("format")]
		public FieldDataStringFormat? Format { get; set; }
	}
}
