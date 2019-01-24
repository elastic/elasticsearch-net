namespace Nest
{
	public class AutoDateHistogramAggregate : MultiBucketAggregate<DateHistogramBucket>
	{
		public Time Interval { get; internal set; }
	}
}
