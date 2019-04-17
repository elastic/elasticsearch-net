using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[ReadAs(typeof(PutWatchResponse))]
	public class PutWatchResponse : ResponseBase
	{
		[DataMember(Name = "created")]
		public bool Created { get; internal set; }

		[DataMember(Name = "_id")]
		public string Id { get; internal set; }

		[DataMember(Name = "_version")]
		public int Version { get; internal set; }
	}
}
