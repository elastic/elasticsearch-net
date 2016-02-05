
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class FiltersBucket : BucketBase
	{
		[JsonProperty("doc_count")]
		public long DocCount { get; set; }

		internal override bool Matches(JToken source)
		{
			throw new NotImplementedException();
		}
	}

	public class FiltersAggregate : MultiBucketAggregate<FiltersBucket>
	{
		public SingleBucketAggregate NamedBucket(string key) => this.Global(key);

		public List<FiltersBucket> AnonymousBuckets() => this.Buckets?.OfType<FiltersBucket>().ToList();
	}
}
