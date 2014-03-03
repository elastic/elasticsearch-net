namespace Nest
{
	public class KeyItem : BucketAggregationBase, IBucketItem
	{
		public string Key { get; set; }
		public long DocCount { get; set; }
	}
}