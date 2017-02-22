using Newtonsoft.Json;

namespace Nest_5_2_0
{

	public class ElasticsearchVersionInfo
	{
		public string Number { get; set; }

		[JsonProperty(PropertyName = "snapshot_build")]
		public bool IsSnapShotBuild { get; set; }

		[JsonProperty(PropertyName = "lucene_version")]
		public string LuceneVersion { get; set; }
	}
}
