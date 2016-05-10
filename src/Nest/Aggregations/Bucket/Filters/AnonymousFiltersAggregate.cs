
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FiltersBucket : BucketBase
	{
		[JsonProperty("doc_count")]
		public long DocCount { get; internal set; }

		internal override bool Matches(JToken source) => false;
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class AnonymousFiltersAggregate : MultiBucketAggregate<FiltersBucket> { }
}
