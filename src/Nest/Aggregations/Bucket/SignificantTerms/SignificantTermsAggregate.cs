namespace Nest
{
	public class SignificantTermsAggregate<TKey> : MultiBucketAggregate<SignificantTermsBucket<TKey>>
	{
		public long? BgCount { get; set; }
		public long DocCount { get; set; }
	}
}
