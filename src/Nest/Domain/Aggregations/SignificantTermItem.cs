namespace Nest
{
	public class SignificantTermItem : BucketAggregationBase, IBucketItem
	{
		public string Key { get; set; }
		public long BgCount { get; set; }
		public long DocCount { get; set; }
		public double Score { get; set; }
	}
}