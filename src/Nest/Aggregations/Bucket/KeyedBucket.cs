using System.Collections.Generic;

namespace Nest
{
	public class KeyedBucket : SingleBucket, IBucketItem
	{
		public KeyedBucket() { }
		public KeyedBucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public string Key { get; set; }
		public string KeyAsString { get; set; }
	}
}
