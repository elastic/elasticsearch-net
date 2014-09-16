using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public abstract class FieldDataMapping
	{
		[JsonProperty("loading")]
		public FieldDataLoading? Loading { get; set; }

		[JsonProperty("filter")]
		public FieldDataFilter Filter { get; set; }
	}
}
