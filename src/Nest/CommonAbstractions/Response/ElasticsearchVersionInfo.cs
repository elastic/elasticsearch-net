using System.Runtime.Serialization;

namespace Nest
{
	public class ElasticsearchVersionInfo
	{
		[DataMember(Name ="snapshot_build")]
		public bool IsSnapShotBuild { get; set; }

		[DataMember(Name ="lucene_version")]
		public string LuceneVersion { get; set; }

		public string Number { get; set; }
	}
}
