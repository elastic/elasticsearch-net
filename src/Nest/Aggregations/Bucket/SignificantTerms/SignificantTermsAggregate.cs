namespace Nest
{
	public class SignificantTermsAggregate : MultiBucketAggregate<SignificantTermsBucket>
	{
		/// <summary>
		/// The background count
		/// </summary>
		public long? BgCount { get; set; }

		/// <summary>
		/// The document count
		/// </summary>
		public long DocCount { get; set; }
	}
}
