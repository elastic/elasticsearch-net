using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	public interface IBucketItem { }

	public abstract class BucketItemBase : AggregationsHelper, IBucketItem
	{
		protected BucketItemBase() { }
		protected BucketItemBase(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }
	}

	public class KeyedBucketItem : BucketItemBase
	{
		public KeyedBucketItem() { }
		public KeyedBucketItem(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public string Key { get; set; }
		public string KeyAsString { get; set; }
		public long? DocCount { get; set; }
	}
}
