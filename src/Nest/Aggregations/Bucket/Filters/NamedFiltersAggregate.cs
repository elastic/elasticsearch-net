
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class NamedFiltersAggregate : BucketAggregateBase
	{
		[JsonProperty("buckets")]
		public IDictionary<string, FiltersBucket> Buckets { get; set; }
	}
}
