using System.Runtime.Serialization;

namespace Nest
{
	public class PutWatchResponse : ResponseBase
	{
		[DataMember(Name = "created")]
		public bool Created { get; internal set; }

		[DataMember(Name = "_id")]
		public string Id { get; internal set; }

		[DataMember(Name = "_version")]
		public int Version { get; internal set; }

		[DataMember(Name = "_seq_no")]
		public long SequenceNumber { get; internal set; }

		[DataMember(Name = "_primary_term")]
		public long PrimaryTerm { get; internal set; }
	}
}
