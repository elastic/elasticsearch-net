using System.Runtime.Serialization;

namespace Nest
{
	public interface IRootNodeInfoResponse : IResponse
	{
		string Name { get; }
		string Tagline { get; }
		ElasticsearchVersionInfo Version { get; }
	}

	[DataContract]
	public class RootNodeInfoResponse : ResponseBase, IRootNodeInfoResponse
	{
		[DataMember(Name ="name")]
		public string Name { get; internal set; }

		[DataMember(Name ="tagline")]
		public string Tagline { get; internal set; }

		[DataMember(Name ="version")]
		public ElasticsearchVersionInfo Version { get; internal set; }
	}
}
