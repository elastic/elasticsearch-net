using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	public class KeyedBucket : BucketBase
	{
		public KeyedBucket() { }
		public KeyedBucket(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public string Key { get; set; }
		public string KeyAsString { get; set; }
		public long? DocCount { get; set; }
	}
}
