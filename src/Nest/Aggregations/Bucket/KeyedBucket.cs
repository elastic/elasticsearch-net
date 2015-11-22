using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public class KeyedBucket : BucketBase, IBucketItem
	{
		public KeyedBucket() { }
		public KeyedBucket(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }

		public string Key { get; set; }
		public string KeyAsString { get; set; }
		public long DocCount { get; set; }
	}
}
