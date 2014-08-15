namespace Nest
{
	public class KeyItem : BucketAggregationBase, IBucketItem
	{
		public string Key { get; set; }
		public string KeyAsString { get; set; }
		public long DocCount { get; set; }
	}
}