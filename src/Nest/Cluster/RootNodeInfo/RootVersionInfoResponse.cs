using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class RootNodeInfoResponse : ResponseBase
	{
		[DataMember(Name ="name")]
		public string Name { get; internal set; }
		
		[DataMember(Name ="cluster_name")]
		public string ClusterName { get; internal set; }

		[DataMember(Name ="cluster_uuid")]
		public string ClusterUUID { get; internal set; }

		[DataMember(Name ="version")]
		public ElasticsearchVersionInfo Version { get; internal set; }
		
		[DataMember(Name ="tagline")]
		public string Tagline { get; internal set; }

	}
}
