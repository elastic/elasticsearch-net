using Newtonsoft.Json;

namespace Nest
{
	public class ElasticsearchVersionInfo
	{
		[JsonProperty(PropertyName = "snapshot_build")]
		public bool IsSnapShotBuild { get; set; }

		[JsonProperty(PropertyName = "lucene_version")]
		public string LuceneVersion { get; set; }

		public string Number { get; set; }
	}
}
