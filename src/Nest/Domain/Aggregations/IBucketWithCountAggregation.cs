namespace Nest
{
	public interface IBucketWithCountAggregation : IBucketAggregation
	{
		long DocCount { get; }
	}
}