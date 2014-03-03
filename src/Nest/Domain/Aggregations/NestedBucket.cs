using Newtonsoft.Json;

namespace Nest
{
	public class SingleBucket : BucketAggregationBase
	{
		[JsonProperty("doc_count")]
		public long DocCount { get; set; }
	}
}